using UnityEngine;


public class Nightborne : EnemyAI
{
    float _attackTimer = 0f;
    float _patrolTimer = 0f;
    float randomDir;

    public override void Start() {
        base.Start();
        randomDir = Random.Range(-1,1);
    }

    public override void Patrolling()
    {
        _patrolTimer -= Time.deltaTime;
        if(_patrolTimer < 0f) {
            Vector2 velocity = new Vector2(randomDir * moveSpeed, rb.velocity.y);
            rb.velocity = velocity;
            if(_patrolTimer < -1f) {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                randomDir = Random.Range(-1,1);
                _patrolTimer = patrolCoolDown;
            }
        }
    }

    public override void Chasing()
    {
        Vector3 direction = (DetectionZone.gameObject.transform.position - transform.position).normalized;
        float finalDirection = direction.x == 0 ? 0 : Mathf.Sign(direction.x); 
        Vector2 move = new Vector2(finalDirection * moveSpeed, rb.velocity.y);
        rb.velocity = move;
    }


    public override void Attacking() {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        _attackTimer -= Time.deltaTime;
        if(_attackTimer < 0) {
            animator.Play("attack");
            _attackTimer = attackCoolDown;
        }
    }

    public void StartAttack() {
        Collider2D hitbox = Physics2D.OverlapBox(transform.position + (transform.right * HitboxOffset), HitboxSize, 0f, plrMask);
        if(hitbox) {
            Entity ent = hitbox.gameObject.GetComponent<Entity>();
            ent?.Damage(damage, Vector2.up);
        }
    }

}
