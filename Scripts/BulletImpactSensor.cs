using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactSensor : MonoBehaviour {
    public SwingKinematic swingKinematic;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet")) {
            swingKinematic.BulletCollision(other);
        }

    }
}
