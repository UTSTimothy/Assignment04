using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class InputManager : MonoBehaviour
{   
    private Vector3 movement; //2D so ignore z axis
    private bool jump;
    private bool attack; 
    private RaycastHit2D hit; 
    private bool canDoubleJump = false;
    private bool started = false; 
    private float nextFootStep = 0;
    private float footstepDelay = 0.3f; 
    public bool grounded = false; 
    public bool doubleJumpUnlocked = false; 
    public float speed ; 
    public float originalSpeed; 
    public float originalJump; 
    public float jumpHeight;
    public Rigidbody2D rb; 
    public Animator animator; //Animations will probs go in seperate script later
    public LevelManager scManager; 
    
    // Start is called before the first frame update
    void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        scManager = GetComponent<LevelManager>();
        Time.timeScale = 0.0f; 
        
    }

    // Update is called once per frame
    void Update()
    {   
        //When player moves unpause game
        if (Input.GetButtonDown("Horizontal") && started == false) {
            Time.timeScale = 1.0f;
            started = true; 
        }

        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0) {

            scManager.LoadA(scManager.currentSceneNumber());
            /*
            //Keep item boost, but makes enemies respawn and hp reset more annoying.
            transform.position = gameObject.GetComponent<PlayerManager>().spawn; 
            gameObject.GetComponent<PlayerManager>().energy = 101; 
            gameObject.GetComponent<PlayerManager>().uiText.text = "";
            Time.timeScale = 1.0f;
            */
        }
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 10);
        //Debug.Log(hit.distance + "distance from collider");
        //Debug.DrawRay(transform.position, dwn*10, Color.red, 0.5f);
        //Debug.Log("collider hit" + hit.collider.tag);
        //Debug.Log(rb.velocity);
        GetMovementInput();
        MoveObject(); 
        Rotate();
    }

    public void GetMovementInput(){
        //continuous values between -1, 1 
        //will need to clamp values for maintain values of 1 when diagonals occur
        movement.x = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Vertical"); //jump should be instant
        attack = Input.GetKeyDown("space");
        //clamping uneccessary if just horizontal movement
        //movement = Vector3.ClampMagnitude(movement, 1.0f);
        //Debug.Log(movement.sqrMagnitude); 
        //jump = Input.GetButtonDown("Jump");
    }

    public void MoveObject(){
        //framerate independent movement (not affected by framerate)
        //transform is object script is attached to
        //time.delta makes this framerate independent
        Attack();
        if (!(jump) && movement.x != 0){
            transform.position += movement * speed * Time.deltaTime;
            animator.SetTrigger("Run"); //trigger running animation.
            if(rb.velocity.y == 0){
                nextFootStep -= Time.deltaTime; 
                if(nextFootStep <= 0){
                gameObject.GetComponent<SoundManager>().aSource.PlayOneShot(
                    gameObject.GetComponent<SoundManager>().walkClip, 0.5f);
                nextFootStep += footstepDelay; 
                }
            }
            
        
        } else if (jump && grounded) {
            //boolean changed via playermanager.
            //with addforce height should == 19, with vel == 22; Scene3 only. 
            animator.SetTrigger("Jump");
            gameObject.GetComponent<SoundManager>().JumpSound();
            rb.velocity = new Vector2(0, jumpHeight); 
            //rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            grounded = false; 
            canDoubleJump = true; 
            
                
            
        } else if (jump && canDoubleJump && doubleJumpUnlocked) {
                canDoubleJump = false; 
                gameObject.GetComponent<SoundManager>().JumpSound();
                rb.velocity = new Vector2(0, jumpHeight);        
        }
        else {
            //when player stops moving set back to idle
            //Debug.Log(hit.distance);
            animator.SetTrigger("Idle");
            Attack();
             
            }
    }

    public void Rotate(){
        //player is moving right
        if (movement.x != 0){
                //Debug.Log(movement.x); 
                if (movement.x > 0) //if player is going right (position)
                {
                    //transform.LookAt(Vector3.right);
                    //up direction is y axis hence vector3.up
                    gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0,0, movement.x), Vector3.up);
               
                } else if (movement.x < 0) //if player is going left 
                {   
                    //transform.LookAt(Vector3.left);
                    gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0,0,movement.x ),  Vector3.up);
                }     
        } 
    }

    public void Attack(){
        if (attack && Time.timeScale != 0.0f){
                animator.SetTrigger("Attack");
                gameObject.GetComponent<SoundManager>().AttackSound();
                gameObject.GetComponent<PlayerManager>().swordCollider.enabled = true; 
                gameObject.GetComponent<PlayerManager>().energy -= 5;  
            } 

    }

}
