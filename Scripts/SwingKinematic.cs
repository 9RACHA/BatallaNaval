using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingKinematic : MonoBehaviour {
    public float restorationTorqueRate = 1;
    Vector3 angularVelocity = Vector3.zero;

    public float impactTime;
    public float impactForce;

    Vector3 impactAcceleration;
    float impactStartTime;


    // Start is called before the first frame update
    void Start() {
        //Para evitar que al principio del juego se aplique la aceleracion de impacto
        impactStartTime = -100 * impactTime;
    }

    // Update is called once per frame
    void Update() {
        //Angulo de inclinación del barco respecto de la vertical
        float leanAngle = Vector3.Angle(Vector3.up, transform.up);

        Vector3 angularAcceleration = Vector3.Cross(transform.up, Vector3.up) * restorationTorqueRate * leanAngle;
        angularAcceleration = transform.InverseTransformDirection(angularAcceleration);
        if(Time.time - impactStartTime  < impactTime) {
            angularAcceleration += impactAcceleration;
        }

        
        Debug.Log("Update angularAcceleration " + angularAcceleration);

        angularVelocity += angularAcceleration * Time.deltaTime;
        

        Debug.Log("Update angularVelocity " + angularVelocity);

        transform.Rotate(angularVelocity * Time.deltaTime);        
    }


    public void BulletCollision(Collision other) {
        return;
        //Damos un empujon de rotación teniendo en cuenta la dirección de movimiento de la bala
        //Proyectamos la dirección de movimiento de la bala en el plano XY, que, en el método
        //Vector3.ProjectOnPlane, se representa por su vector normal, Z
        impactAcceleration = Vector3.Cross(Vector3.up, Vector3.ProjectOnPlane(other.gameObject.transform.forward, transform.forward)).normalized * impactForce;
        impactStartTime = Time.time;
        impactAcceleration = transform.InverseTransformVector(impactAcceleration);
        Debug.Log("BulletCollision impactAcceleration " + impactAcceleration);
    }
}
