using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLeapState : StateMachineBehaviour
{
    Boss boss;
    Player player;
    Vector3 startPos;
    Vector3 targetPos;
    float elapsedTime = 0;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        player = FindAnyObjectByType<Player>();
        boss.rb.velocity = new Vector2(0f, 0f);

        startPos = animator.transform.position;
        targetPos = startPos + (Vector3.up * 4f);
        elapsedTime = 0f;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss.leaping) { 
            elapsedTime += Time.deltaTime;
            animator.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime * 8f);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(boss.rb != null) { boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y); }
    }
}
