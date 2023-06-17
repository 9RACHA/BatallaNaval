using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraController : MonoBehaviour {
    public Transform cannonTower;
    public Transform panCameraTransform;
    public Transform ship;
    private Transform targetBulletTransform;

    private Quaternion startRotation;
    private float startFieldOfView;

    private Camera camera;
    // Start is called before the first frame update
    void Start() {
        targetBulletTransform = null;
        camera = panCameraTransform.GetComponent<Camera>();  
 
        startRotation = panCameraTransform.transform.localRotation;
        startFieldOfView = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update() {
        Vector3 eulerAngles = cannonTower.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.eulerAngles = eulerAngles;
        if(targetBulletTransform != null) {            
            //Orientamos la cámara hacia la bala;
            Vector3 lookDirection = targetBulletTransform.position - panCameraTransform.position;
            float distance = lookDirection.magnitude;
            panCameraTransform.rotation = Quaternion.LookRotation(lookDirection);
            camera.fieldOfView = 5000 / distance;
        }


        //Situamos el PanCameraMount en las coordenadas X y Z del barco,
        //sin cambiar la coordenada Y
        Vector3 position = transform.position;
        position.x = ship.position.x;
        position.z = ship.position.z;
        transform.position = position;


        
    }

    public void FollowBullet(Transform bullet) {
        targetBulletTransform = bullet;
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if(bulletComponent != null) {
            bulletComponent.OnBulletDestroyed += OnTargetBulletDestroyed;
        }

    }

    private void OnTargetBulletDestroyed(GameObject bullet) {
        Debug.Log("PanCamera.OnTargetBulletDestroyed");
        Invoke("RestoreStartValues", 2f);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if(bulletComponent != null) {
            bulletComponent.OnBulletDestroyed -= OnTargetBulletDestroyed;
        }
    }

    private void RestoreStartValues() {
        panCameraTransform.localRotation = startRotation;
        camera.fieldOfView = startFieldOfView;
    }
}
