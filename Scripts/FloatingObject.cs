using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{   
    private float yPos;
    private Vector3 vec;
    
    // Start is called before the first frame update
    void Start()
    {
        vec = transform.position; 
        yPos = vec.y; 
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(0, Random.Range(-2.0f, 2.0f));
        vec.y = (yPos + (float)0.2*Mathf.Sin(2*Time.time));
        transform.position = vec; 
        //https://answers.unity.com/questions/59934/how-to-an-object-floating-up-and-down.html
    }

    
}
