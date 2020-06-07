using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rb; 

    public Vector3 spawn; 
    public float energy = 101; 
    public Text energyText;
    public TextMeshProUGUI uiText; 
    public GameObject speedObj;
    public GameObject healthObj;
    public Collider2D swordCollider;
    
    void Start()
    {
       //swordCollider = GetComponent<Collider2D>(); 
       spawn = transform.position; 
       rb = GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void Update()
    {  
        if(energy > 101) {
            energy = 101;
        } else if (energy < 1) {
            Time.timeScale = 0f; 
            uiText.text = "You Died \n Press R to restart level";
        }

        energy -= Time.deltaTime;
        energyText.text = "Energy: " + (int)energy; 

        if (energy < 0) {
            energyText.text = "Energy: 0"; 
        }
        
        
    }
    //script is attached to player, so whatever it touches
    void OnTriggerEnter2D(Collider2D other) { 
        //Debug.Log("touchOther");
        if (other.gameObject.name == "HealthBoost")
        {
            if (isScene(0)){
                uiText.text = "Touch the Lightning for a Speed Boost!";
            } 
            Destroy(other.gameObject);
            //If the GameObject has the same tag as specified, output this message in the console
            //Debug.Log("touch");
            energy += 20; 
            Debug.Log(energy + "Energy");
        } else if (other.gameObject.tag == "Health") {
            Destroy(other.gameObject);
            energy += 30; 
            //Respawnable health item
            if(other.gameObject.name == "HealthBoostRe" || other.gameObject.name == "HealthBoost(Clone)"){
                StartCoroutine(RespawnObject(healthObj));
            }
            //Debug.Log(energy + "Energy");
        }
        if (other.gameObject.name == "SpeedBoost" ||other.gameObject.name == "SpeedBoost(Clone)") {
            if (isScene(0)){
                uiText.text = "Avoid the enemy or lose hp!";
            } 
            //gameObject.GetComponent<InputManager>().speed = gameObject.GetComponent<InputManager>().speed*2;
            //Vector2 playerCurrentVel = new Vector2(); 
            //playerCurrentVel = gameObject.GetComponent<InputManager>().rb.velocity;
            //get direction of player so they can boost left or right. 
            //gameObject.GetComponent<InputManager>().rb.velocity = new Vector2(Input.GetAxis("Horizontal")*6, 1);
            gameObject.GetComponent<InputManager>().speed = gameObject.GetComponent<InputManager>().speed*2; //increase speed 
            gameObject.GetComponent<InputManager>().jumpHeight = gameObject.GetComponent<InputManager>().jumpHeight*1.2f;
            StartCoroutine(SpeedReset());
            StartCoroutine(RespawnObject(speedObj));
            StartCoroutine(ResetText());
            Destroy(other.gameObject); 
            uiText.text = "Speed Increase";
            
            //other.gameObject.GetComponent<Renderer>().enabled = false; //remove object; really just rendering
           
        }
        if (other.gameObject.tag == "Bottom") {
            energy -= 100; 
        }
        if (other.gameObject.name == "Step1") {
            uiText.text = "Jump over objects with the arrow using W or Up Arrow key!";
        }
        if (other.gameObject.name == "Arrow") {
            uiText.text = "Touch the Coffee Bean to recover your Health!";
        }
        if (other.gameObject.tag == "Background" || other.gameObject.tag == "Obstacle"){
            //print("touched Background"+ other.gameObject);
            gameObject.GetComponent<InputManager>().grounded = true; 
        }
        if (other.gameObject.name == "DoubleJump") {
            gameObject.GetComponent<InputManager>().doubleJumpUnlocked = true; 
            uiText.text = "Double Jump unlocked";
            StartCoroutine(ResetText());
            Destroy(other.gameObject); 
        }
        if (other.gameObject.name == "CoffeeCup") {
            //change this to load next level
            //via a couroutine 
            uiText.text = "You win! \n Press ESC to exit";
            gameObject.GetComponent<SoundManager>().WinSound();
            Time.timeScale = 0f; //freeze time so game wont run. 
        }
        if (other.gameObject.name == "HellPortal") {
            //testing.
            uiText.text = "Gateway to Hell Unlocked";
            gameObject.GetComponent<LevelManager>().DelayedLoad(0);
        }
        if (other.gameObject.tag == "Health" || other.gameObject.tag == "Item"){
            gameObject.GetComponent<SoundManager>().BoostSound();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Enemy"){
            gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            gameObject.GetComponent<SoundManager>().HurtSound();
            energy -= 10;
        
            //Get direction of player
            float heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;
            if (heading == 0f) {
                heading -= 180f;    
            }
            heading = heading/180; 
            print(heading);
            //Use direction to push player in opposite direction
            //when in colliding with enemy. 
            rb.AddForce(new Vector2(heading * 4f, 0),ForceMode2D.Impulse);
        }
        if (other.gameObject.tag == "Background" || other.gameObject.tag == "Obstacle" ){
            //print("touched Background"+ other.gameObject);
            gameObject.GetComponent<InputManager>().grounded = true; 
        }
        if (other.gameObject.tag == "Spike"){
            gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            gameObject.GetComponent<SoundManager>().HurtSound();
            energy -= 5; 
        }

        
    }
    
    /*
    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Spike"){
            energy -= (int)0.1; 
        }
    } 
    */

    private bool isScene(int num){
        if (gameObject.GetComponent<LevelManager>().currentSceneNumber() == num) {
            return true; 
        } else {
            return false; 
        }
    }
    //coroutine 
    IEnumerator RespawnObject(GameObject gameObject){
        yield return new WaitForSeconds(5);
        GameObject newObj = Instantiate(gameObject);
    }

    IEnumerator SpeedReset(){
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<InputManager>().speed = gameObject.GetComponent<InputManager>().originalSpeed;
        gameObject.GetComponent<InputManager>().jumpHeight = gameObject.GetComponent<InputManager>().originalJump;
    }

    IEnumerator ResetText(){
        yield return new WaitForSeconds(2);
        uiText.text = "";
    }

    IEnumerator DelayReset(){
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<InputManager>().grounded = true; 
    }
}
