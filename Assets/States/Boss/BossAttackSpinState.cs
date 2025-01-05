using UnityEngine;

public class BossAttackSpinState : StateMachineBehaviour
{
    Boss boss;
    Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        player = FindAnyObjectByType<Player>();
        boss.rb.velocity = new Vector2(0f, 0f);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss.facingRight) {
            boss.rb.velocity = new Vector2(boss.spinSpeed * 1, 0f);
        }

        if(!boss.facingRight) {
            boss.rb.velocity = new Vector2(boss.spinSpeed * -1, 0f);
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(boss.rb != null) { boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y); }
    }

 }
