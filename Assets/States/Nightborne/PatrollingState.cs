using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : StateMachineBehaviour
{

    Enemy enemy;
    float _timer;
    float _direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.5f;
        enemy = animator.GetComponent<Enemy>();
        if(enemy.facingRight) { _direction = -1f; }
        if(!enemy.facingRight) { _direction = 1f; }
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer -= Time.deltaTime;
        Vector2 velocity = new Vector2(_direction * enemy.moveSpeed, enemy.rb.velocity.y);
        enemy.rb.velocity = velocity;
        if(enemy.DetectionZone) { 
            int layerMasks = LayerMask.GetMask("Ground", "Player");
            Vector3 direction = enemy.DetectionZone.transform.position - animator.transform.position;
            RaycastHit2D raycast = Physics2D.Raycast(animator.transform.position, direction, 50f, layerMasks);

            if(raycast.collider != null && raycast.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
                animator.SetBool("Chasing", true);
            } 
            
        }
        if(_timer <= 0) { animator.SetBool("Patrolling", false); }
        enemy.flipSprite();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.rb.velocity = new Vector2(0f, enemy.rb.velocity.y);
    }
}
