using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRecoil : MonoBehaviour {
    public float recoilSpeed = 4f;
    public float restorationAcceleration = 5;
    public float maxRestorationSpeed = 20;
    float speed = 0f;
    float startZ;

    private bool cannonMoving;

    // Start is called before the first frame update

    void Start() {
        startZ = transform.localPosition.z;    
        cannonMoving = false;
    }

    // Update is called once per frame
    void Update() {
        
        if(cannonMoving) {
            float distance = startZ - transform.localPosition.z;
            if(speed < 0) {
                //Fase de retroceso
                speed += restorationAcceleration * distance * Time.deltaTime;
            } else {
                //Fase de recuperacion
                speed += restorationAcceleration * distance * Time.deltaTime;
                speed = Mathf.Clamp(speed, 0, maxRestorationSpeed);
                if(distance < 0) {
                    Stop();
                }
            }

            transform.localPosition += Vector3.forward * speed * Time.deltaTime;
        } 
        
    }

    private void Stop() {
        speed = 0;
        cannonMoving = false;
        Vector3 localPosition = transform.localPosition;
        localPosition.z = startZ;
        transform.localPosition = localPosition;
    }

    public void Recoil() {
        speed  = -recoilSpeed;
        cannonMoving = true;
    }
}
