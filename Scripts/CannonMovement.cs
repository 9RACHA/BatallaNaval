using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour {
    public Transform cannonPivot;
    public Transform leftCannonShotPoint;
    public Transform rightCannonShotPoint;

    public GameObject bulletPrefab;

    private float towerRotationSpeed = 80;
    private float cannonRotationSpeed = 60;
    private float cannonSpeedCorrection = 0.2f;

    private int towerMoving = 0;
    private int cannonMoving = 0;

    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float actualSpeedCorrection = 1;
        if(Input.GetKey(KeyCode.LeftControl)) {
            actualSpeedCorrection = cannonSpeedCorrection;
        }
        //Mientras se presionen las flechas derecha e izquierda,
        //giramos la torre en horizontal
        if(Input.GetKey(KeyCode.LeftArrow) && towerMoving <= 0) {
            towerMoving = -1;
            transform.Rotate(-Vector3.up * towerRotationSpeed * actualSpeedCorrection * Time.deltaTime);
        } else if(Input.GetKey(KeyCode.RightArrow) && towerMoving >= 0) {
            towerMoving = 1;
            transform.Rotate(Vector3.up * towerRotationSpeed * actualSpeedCorrection * Time.deltaTime);
        } else {
            towerMoving = 0;
        }

        //Mientras se presionen las flechas arriba y abajo
        //giramos los cañones en torno al eje X para subirlos y bajarlos
        if(Input.GetKey(KeyCode.UpArrow) && cannonMoving <= 0) { 
            cannonMoving = -1;

            if(cannonPivot.localEulerAngles.x > 300f || cannonPivot.localEulerAngles.x < 10) {
                cannonPivot.Rotate(-Vector3.right * cannonRotationSpeed * actualSpeedCorrection * Time.deltaTime);
            }
        } else if(Input.GetKey(KeyCode.DownArrow) && cannonMoving >= 0) { 
            cannonMoving = 1;

            if(cannonPivot.localEulerAngles.x > 180f) {             
                cannonPivot.Rotate(Vector3.right * cannonRotationSpeed * actualSpeedCorrection * Time.deltaTime);
            }
        } else {
            cannonMoving = 0;

        }

    }


}
