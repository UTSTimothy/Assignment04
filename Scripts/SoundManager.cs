using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource aSource; 
    public AudioClip attackClip;
    public AudioClip hurtClip;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip boostClip;
    public AudioClip winClip;  
    public AudioClip walkClip;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AttackSound(){
        aSource.clip = attackClip;
        aSource.Play();
        
    }
    public void HurtSound(){
        aSource.clip = hurtClip;
        aSource.Play();
    }
    public void JumpSound(){
        aSource.clip = jumpClip;
        aSource.Play();
    }
    public void DeathSound(){
        aSource.clip = deathClip;
        aSource.Play();
    }
    public void BoostSound(){
        aSource.clip = boostClip;
        aSource.Play();
    }

    public void WinSound(){
        aSource.clip = winClip;
        aSource.Play();
    }
    public void WalkSound(){
        aSource.clip = walkClip;
        aSource.PlayOneShot(walkClip, 0.3f);
    }

    
}
