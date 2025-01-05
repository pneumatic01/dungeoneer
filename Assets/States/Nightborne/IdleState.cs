using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
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
        if(enemy.DetectionZone) { 
            int layerMasks = LayerMask.GetMask("Ground", "Player");
            Vector3 direction = enemy.DetectionZone.transform.position - animator.transform.position;
            RaycastHit2D raycast = Physics2D.Raycast(animator.transform.position, direction, 50f, layerMasks);
            if(raycast.collider.gameObject.layer == LayerMask.GetMask("Player")) {
                animator.SetBool("Chasing", true);
            } 
            
        }
        if(_timer <= 0) { animator.SetBool("Patrolling", true); }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
