using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DeathAnim")) {
            //Destroy(this, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
