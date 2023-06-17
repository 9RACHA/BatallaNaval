using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
    private const int MAX_POWER = 6;
    private const int MIN_POWER = 0;
    private const int MAX_RUDDER = 3;
    private float baseSpeed = 0.8f;
    private int powerLevel = 0;

    private float effectiveSpeed;

    private float baseRotationSpeed = 1.2f;
    private float baseRudderDrag = 0.2f;
    private int rudderLevel = 0;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.W)) {
            powerLevel++;
            if(powerLevel > MAX_POWER) {
                powerLevel = MAX_POWER;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            powerLevel--;
            if(powerLevel < MIN_POWER) {
                powerLevel = MIN_POWER;
            }
        }


        if(Input.GetKeyDown(KeyCode.D)) {
            rudderLevel++;
            if(rudderLevel > MAX_RUDDER) {
                rudderLevel = MAX_RUDDER;
            }
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            rudderLevel--;
            if(rudderLevel < -MAX_RUDDER) {
                rudderLevel = -MAX_RUDDER;
            }
        }

        if(powerLevel != 0) { 
            transform.Rotate(Vector3.up * baseRotationSpeed * rudderLevel * Time.deltaTime, Space.World);
        }
        effectiveSpeed = baseSpeed * powerLevel;
        effectiveSpeed -= Mathf.Abs(rudderLevel) * baseRudderDrag;
        effectiveSpeed = effectiveSpeed<0?0:effectiveSpeed;
        transform.position += transform.forward * effectiveSpeed * Time.deltaTime;
        
    }


    void OnGUI() {
        GUI.color = Color.black;
        GUI.Label(new Rect(10, Screen.height-50, 200, 40), "Power Level " + powerLevel);
        GUI.Label(new Rect(220, Screen.height-50, 200, 40), "Rudder Level " + rudderLevel);
        //Velocidad del barco en nudos 
        GUI.Label(new Rect(420, Screen.height-50, 200, 40), "Speed " + effectiveSpeed * 1.944f);
     }
}
