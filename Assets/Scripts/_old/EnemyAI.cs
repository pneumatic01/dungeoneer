using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Entity
{
    [Header("Enemy Stats")]
    public int damage = 25;
    public int attackCoolDown = 2;
    public int patrolCoolDown = 4;
    public float moveSpeed = 5f;

    protected enum States {Patrolling, Attacking, Chasing};
    protected States currentState;

    [Header("State Values")]
    public float AtkRange;
    public float DetectionRange;
    public Vector2 HitboxSize;
    public float HitboxOffset;
    public LayerMask plrMask;

    protected Collider2D DetectionZone;
    protected Collider2D AttackingZone;
  
    public virtual void Start() {
        currentState = States.Patrolling;
    }

    void Update() {
        DetectionZone = Physics2D.OverlapCircle(transform.position, DetectionRange, plrMask);
        AttackingZone = Physics2D.OverlapCircle(transform.position, AtkRange, plrMask);
        SwitchState(DetectionZone, AttackingZone);

        if(currentState == States.Patrolling) { Patrolling(); }
        if(currentState == States.Chasing) { Chasing(); }
        if(currentState == States.Attacking) { Attacking(); }

        flipSprite();
        AnimationHandle();
    }

    void SwitchState(Collider2D detectZone, Collider2D atkZone) {
        if(!detectZone && !atkZone) {
            currentState = States.Patrolling;
        }

        if(detectZone) {
            currentState = States.Chasing;
        }

        if(detectZone && atkZone) {
            currentState = States.Attacking;
        }
    }

    public virtual void Patrolling() {}
    public virtual void Chasing() {}
    public virtual void Attacking() {}

    void flipSprite() {
        if(rb.velocity.x > 0.1f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if(rb.velocity.x < -0.1f)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void AnimationHandle()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtkRange);
        Gizmos.DrawCube(transform.position + (transform.right * HitboxOffset), HitboxSize);
    }
}
