using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    Enemy enemy;
    Vector3 _direction;
    float speedMultiplier = 2f;
    float aggroMultiplier = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy = animator.GetComponent<Enemy>();
       enemy.moveSpeed *= speedMultiplier;
       enemy.DetectionRange *= aggroMultiplier;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy.DetectionZone != null) {
           _direction = (enemy.DetectionZone.gameObject.transform.position - animator.transform.position).normalized; 
        }
        float finalDirection = _direction.x == 0 ? 0 : Mathf.Sign(_direction.x); 
        Vector2 move = new Vector2(finalDirection * enemy.moveSpeed, enemy.rb.velocity.y);
        enemy.rb.velocity = move;
        enemy.flipSprite();

        if(!enemy.DetectionZone) {
            animator.SetBool("Chasing", false);
        }

        if(enemy.AttackingZone) {
            animator.SetBool("Attacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.rb.velocity = new Vector2(0f, enemy.rb.velocity.y);
        enemy.moveSpeed /= speedMultiplier;
        enemy.DetectionRange /= aggroMultiplier;
    }

}
