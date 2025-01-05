using UnityEngine;

public class BossChaseState : StateMachineBehaviour
{
    
    Boss boss;
    Player player;
    Vector3 _direction;
    string attackType;
    float attackRange = 2f;
    float attackFarRange = 10f;
    bool CanLeapAttack = false;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss = animator.GetComponent<Boss>();
       player = FindAnyObjectByType<Player>();
       RandomAttack();
       ChanceToLeap();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player == null) { return; } 
        if(player != null) {
            _direction = (FindObjectOfType<Player>().transform.position - animator.transform.position).normalized; 
        }
        float finalDirection = _direction.x == 0 ? 0 : Mathf.Sign(_direction.x); 
        Vector2 move = new Vector2(finalDirection * boss.moveSpeed, boss.rb.velocity.y);

        float distance = Vector2.Distance(player.transform.position, animator.transform.position);
        if(distance <= attackRange) {
            move = new Vector2(0f,boss.rb.velocity.y);
            animator.SetTrigger(attackType);  
        }

        if(distance <= attackFarRange && distance >= (attackFarRange - 2f) && CanLeapAttack){
            animator.SetTrigger("LeapAttack");
            CanLeapAttack = false;
        }

        boss.rb.velocity = move;
        boss.flipSprite();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(boss.rb != null) { boss.rb.velocity = new Vector2(0f, boss.rb.velocity.y); }
        animator.ResetTrigger("SpinAttack");
        animator.ResetTrigger("BaseAttack");
        animator.ResetTrigger("LeapAttack");
    }

    void RandomAttack() {
        int random = Random.Range(0,2);
        if(random  == 0) {
            attackType = "SpinAttack";
        }
        if(random == 1) {
            attackType = "BaseAttack";
        }
    }

    void ChanceToLeap() {
        int random = Random.Range(0,99);
        Debug.Log(random);
        if(random > 30) {
            CanLeapAttack = false;
        }
        if(random <= 30) {
            CanLeapAttack = true;
        }
    }
}
