using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject bulletTrailPrefab;
    public GameObject explosionEffect;

    //Creamos el Delegate que lanzaremos para informar de la explosión de la bala
    public delegate void OnBulletDestroyedDelegate(GameObject bullet);
    public OnBulletDestroyedDelegate OnBulletDestroyed;

    private float startTime;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if(Time.time - startTime > 0.2f) {
            SpawnTrail();
        }
        //Por si nos salimos del mundo, destruimos la bala en caso de que la coordenada
        // Y baje de -100

        Vector3 movementDirection = rb.velocity;
        transform.LookAt(movementDirection);

        if(transform.position.y < -100f) {
            if(OnBulletDestroyed != null) {
                OnBulletDestroyed(gameObject);
            }
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision other){
        GameObject newEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(newEffect, newEffect.GetComponent<ParticleSystem>().main.duration);

        if(OnBulletDestroyed != null) {
            OnBulletDestroyed(gameObject);
        }
        
        Destroy(gameObject);
    }

    private void SpawnTrail() {
        for(int i=0; i<50; i++) {
            Instantiate(bulletTrailPrefab, transform.position - transform.forward*(Random.Range(2, 4f)) + (Vector3)Random.insideUnitCircle*0.3f, Quaternion.identity);
        }
    }
}
