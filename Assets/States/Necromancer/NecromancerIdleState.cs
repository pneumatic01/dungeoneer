using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerIdleState : StateMachineBehaviour
{
    
    float _timer = 3f;
    Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _timer = 3f;
       enemy = animator.GetComponent<Enemy>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer -= Time.deltaTime;
        if(enemy.AttackingZone) { animator.SetBool("Attacking", true); }
        if(_timer <= 0) { animator.SetBool("Patrolling", true); }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
