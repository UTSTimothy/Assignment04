using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Collider2D objCollider;
    // Start is called before the first frame update
    void Start()
    {
        //Will just grab the collider attached to the gameobject
        //so we need to specify which one we want
        //by dragging and dropping in. 
        //objCollider = GetComponent<Collider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
