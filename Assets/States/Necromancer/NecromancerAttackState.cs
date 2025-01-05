
using UnityEngine;

public class NecromancerAttackState : StateMachineBehaviour
{
    Enemy enemy;
    float _cooldownTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        _cooldownTimer = enemy.attackCooldown;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!enemy.AttackingZone) {
            animator.SetBool("Attacking", false);
        }

        _cooldownTimer -= Time.deltaTime;
        if(_cooldownTimer < 0) {
            animator.Play(stateInfo.fullPathHash, 0, 0f);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
