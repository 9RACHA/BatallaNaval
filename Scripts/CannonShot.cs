using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform leftCannonFirePoint;
    public Transform rightCannonFirePoint;

    public CannonRecoil leftCannonRecoil;
    public CannonRecoil rightCannonRecoil;

    public PanCameraController panCameraController;

    private bool loaded = true;

    float shotForce = 800;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(loaded) {
                Shot(leftCannonFirePoint, leftCannonRecoil);
                Shot(rightCannonFirePoint, rightCannonRecoil);
                loaded = false;
                Invoke("Load", 4);

                foreach(ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
                    ps.Play();
                }

            }
        }
        
    }

    private void Shot(Transform cannonFirePoint, CannonRecoil cannonRecoil) {
        GameObject bulletGO = Instantiate(bulletPrefab, cannonFirePoint.position, cannonFirePoint.rotation);
        bulletGO.GetComponent<Rigidbody>().AddForce(cannonFirePoint.transform.forward * shotForce, ForceMode.Impulse);
        cannonRecoil.Recoil();
        if(cannonFirePoint == leftCannonFirePoint) {
           panCameraController.FollowBullet(bulletGO.transform);
        }
    }

    private void Load() {
        loaded = true;
    }

    void OnGUI() {
        GUI.backgroundColor = Color.green;
        if( ! loaded) {
            GUI.backgroundColor = Color.red;
        }

        GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 80, 80), "");
    }

}
