using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Camera mainCamera;
    public Camera panCamera;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.C)) {
            if(mainCamera != null && panCamera != null) {
                //Intercambiamos las cámaras
                Rect auxViewPort = mainCamera.rect;
                float auxDepth = mainCamera.depth;

                mainCamera.rect = panCamera.rect;
                mainCamera.depth = panCamera.depth;

                panCamera.rect = auxViewPort;
                panCamera.depth = auxDepth;
            }
        }        
    }
}
