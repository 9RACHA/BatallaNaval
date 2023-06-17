using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public GameObject bulletTrailPrefab;
    public delegate void ReachedPointDelegate(Vector3 reachedPointCoordinates);
    public event ReachedPointDelegate reachedPoint;

    public GameObject explosionEffect;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {        
        SpawnTrail();

        Vector3 movementDirection = rb.velocity;
        transform.LookAt(movementDirection);
    }

    void OnCollisionEnter(Collision other){
        GameObject newEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(newEffect, newEffect.GetComponent<ParticleSystem>().main.duration);
        if(reachedPoint != null) {
            reachedPoint(transform.position);
        }
        Destroy(gameObject);

        if(other.gameObject.CompareTag("Player")) {
            Vector3 bulletVelocity = GetComponent<Rigidbody>().velocity;            
        }
    }

    private void SpawnTrail() {
        for(int i=0; i<20; i++) {
            Instantiate(bulletTrailPrefab, transform.position - transform.forward*(Random.Range(2, 4f))  + (Vector3)Random.insideUnitCircle*0.3f, Quaternion.identity);
        }
    }
}
