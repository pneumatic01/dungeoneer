using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : StateMachineBehaviour
{

    Boss boss;
    float _cooldownTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        _cooldownTimer = boss.attackCooldown;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!boss.AttackingZone) {
            animator.SetBool("Attacking", false);
        }

        _cooldownTimer -= Time.deltaTime;
        if(_cooldownTimer < 0) {
            animator.Play(stateInfo.fullPathHash, 0, 0f);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(boss.rb != null) { boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y); }
    }
}
