using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuoyancyKinematic : MonoBehaviour {
    public float acceleration = -1;
    float verticalSpeed;
    float topPosition;
    bool subiendo;

    void Start() {
        verticalSpeed = 0;
        //Levanto el barco para que empieze a subir y bajar
        transform.position += transform.up*1.5f;
        topPosition = transform.position.y;
        subiendo = false;
    }

    void Update() {
        /*
        float verticalLevel = transform.position.y;

        verticalSpeed += acceleration * verticalLevel * Time.deltaTime;
        transform.position += transform.up * verticalSpeed * Time.deltaTime;

        //El puro cálculo matemático nos deja errores de redondeo que a la larga
        //hacen que el movimiento de oscilación se vaya amortiguando
        //Para evitarlo, al final de cada subida colocamos el barco de nuevo en 1.5 m
        //de altura y velocidad 0;
        if(subiendo && verticalSpeed < 0) {
            verticalSpeed = 0;
            Vector3 position = transform.position;
            position.y = topPosition;
            transform.position = position;
            subiendo = false;
        }

        if(! subiendo && verticalSpeed > 0) {
            subiendo = true;
        }
        */

        CalculateFinasLonchas();

        
    }

    void CalculateFinasLonchas() {
        float lonchasNumber = 100;

        Vector3 position = transform.position;
        float verticalLevel = position.y;

        for(int i=0; i<lonchasNumber; i++ ) {
            verticalSpeed += acceleration * verticalLevel * Time.deltaTime / lonchasNumber;
            position += transform.up * verticalSpeed * Time.deltaTime / lonchasNumber;
            verticalLevel = position.y;

        }

        transform.position = position;
    }

}
