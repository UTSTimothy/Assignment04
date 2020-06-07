using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float xPos;
    private float yPos; 
    private float min; 
    private float max;
    public float amp = 0; 
    private Vector3 vec;
    private GameObject background; 

    public float health; 
    public Animator animator; 
    public Collider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {   
        if(transform.name != "MiniBoss") {
            health = 100;
        } else if (transform.name == "MiniBoss") {
            health = 200; 
        }
        vec = transform.position; 
        xPos = vec.x; 
        yPos = vec.y; 
        //since amp is the max height of teh sin curve, you add or subtract those
        //from the middle position to get min and max. 
        min = vec.x - amp;
        max = vec.x + amp; 
        background = GameObject.Find("BackgroundGrid");
    }

    // Update is called once per frame
    void Update()
    {
        vec.x = (xPos + (float)amp*Mathf.Sin(1*Time.time));
        transform.position = vec;
        //https://answers.unity.com/questions/59934/how-to-an-object-floating-up-and-down.html
        
        //checks if transform has reached peak amp
        //rotates it the other way.
        if (vec.x >= (max - 0.1)) {
            transform.right = new Vector3(xPos, yPos, 0.0f) - transform.position;
        } else if (vec.x <= min + 0.1) {
            transform.right = new Vector3(xPos, yPos, 0.0f) - transform.position;
        }
        
        Dead();
        
    }

    /*
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Sword"){
            animator.SetTrigger("Hurt");
            health -= 30; 
            print("hitenter");
        }
    }
    */
    
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Sword"){
            animator.SetTrigger("Hurt");
            //disable collider to prevent multiple hits detected from collision.
            enemyCollider.enabled = false; 
            StartCoroutine(colliderReset()); 
            health -= 40; 
            print(health + "monster");
            print("hit");
        }

    }
    

    public void Dead(){
        if (health <= 0) {
            animator.SetTrigger("Dead");
            GameObject.Find("Player").GetComponent<SoundManager>().DeathSound();
            if (transform.name == "MiniBoss") {
                background.GetComponent<GameManager>().objCollider.enabled = false; 
            }
        }

    }

    IEnumerator colliderReset(){
        yield return new WaitForSeconds(1f);
        enemyCollider.enabled = true; 
    }
  
}
