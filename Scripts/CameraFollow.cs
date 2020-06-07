using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public LevelManager scManager; 
    public float size; //size of camera view
    private float yOffset; 
    private bool scenetut, scene1, scene2, scene3, scene4; 
    

    void Awake(){
        scenetut = scene1 = scene2 = scene3 = scene4 = false; 
    } 

    void Start() {
        scManager = GetComponent<LevelManager>();
        yOffset = transform.position.y - player.position.y;
        
        if (scManager.currentSceneName() == "MainScene") {
            size = 2.687657f;
            scenetut = true; 
            GetComponent<UnityEngine.Camera>().orthographicSize = size; //based on inspector value
        } else if (scManager.currentSceneName() == "Level1") {
            size = 8f; 
            scene1 = true;
            GetComponent<UnityEngine.Camera>().orthographicSize = size;
        }
        else if (scManager.currentSceneName() == "Level2") {
            size = 8f; 
            scene2 = true;
            GetComponent<UnityEngine.Camera>().orthographicSize = size;
        }
        else if (scManager.currentSceneName() == "Level3") {
            size = 8f; 
            scene3 = true;
            GetComponent<UnityEngine.Camera>().orthographicSize = size;
        }
        else if (scManager.currentSceneName() == "Level4") {
            size = 8f; 
            scene4 = true;
            GetComponent<UnityEngine.Camera>().orthographicSize = size;
        }
        
    }

    void Update() {
        //transform is the object that the script is attached to.
        //thus itll follow player position 
        //every frame camera placed at players current x,y position
        //y is given offset, because player becomes center of camera, 
        //so we have to make the player be at the bottom of the camera. 
        //while still maintain everything on screen. 
        if (scenetut) {
            transform.position = new Vector3(player.position.x, player.position.y + (transform.position.y - player.position.y), transform.position.z);
        } else if (scene1) {
            transform.position = new Vector3(player.position.x, player.position.y + (transform.position.y - player.position.y), transform.position.z);
        } else if (scene2) {
            transform.position = new Vector3(player.position.x, player.position.y + (transform.position.y - player.position.y), transform.position.z);
        } else if (scene3) {
            transform.position = new Vector3(player.position.x, player.position.y + yOffset , transform.position.z);
        } else if (scene4) {
            transform.position = new Vector3(player.position.x, player.position.y + (transform.position.y - player.position.y), transform.position.z);
        }
       
    }
}
