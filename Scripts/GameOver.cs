using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator.gameObject.GetComponent<PlayerManager>().swordCollider.enabled = false;
        Time.timeScale = 0f; 
    }
}
