using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Enemy Stats")]
    public int damage = 25;
    public float moveSpeed = 5f;
    public bool facingRight = true;
    public int aetherDropMin = 18;
    public int aetherDropMax = 32;
    public float attackCooldown;

    [Header("References")]
    public GameObject deathVFX;
    public GameObject aetherEssence;


    [Header("State Values")]
    public float AtkRange;
    public float DetectionRange;
    public Vector2 RangeOffset;
    public Vector2 HitboxSize;
    public Vector2 HitboxOffset;
    public LayerMask plrMask;

    [HideInInspector] public Collider2D DetectionZone;
    [HideInInspector] public Collider2D AttackingZone;
    private Collider2D hitbox;

    public virtual void Start() {
        rb.gravityScale = 6f;
    }   

    void Update() {
        DetectionZone = Physics2D.OverlapCircle(transform.position + new Vector3(RangeOffset.x, RangeOffset.y, 0f), DetectionRange, plrMask);
        AttackingZone = Physics2D.OverlapCircle(transform.position + new Vector3(RangeOffset.x, RangeOffset.y, 0f), AtkRange, plrMask);
    }

    public void flipSprite() {
        if(rb.velocity.x < -0.1f) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
        if(rb.velocity.x > 0.1f) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
    }

    public void FaceTowardsPlayer() {
        if(AttackingZone != null) {
            if(transform.position.x > AttackingZone.transform.position.x ) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                facingRight = false;
            }
            if(transform.position.x < AttackingZone.transform.position.x ) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                facingRight = true;
            }
        }

    }

    public void StartAttack() {
        hitbox = Physics2D.OverlapBox(transform.position + (transform.right * HitboxOffset.x) + (transform.up * HitboxOffset.y), HitboxSize, 0f, plrMask);

        if(hitbox) {
            Entity ent = hitbox.gameObject.GetComponent<Entity>();
            ent?.Damage(damage, Vector2.up);
        }
    }

    public override void OnDeath()
    {
        SpawnEssence();
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void SpawnEssence() {
        int amount = Random.Range(aetherDropMin, aetherDropMax);
        for (int i = 0; i < amount; i++ ) {
            Instantiate(aetherEssence, transform.position, Quaternion.identity);
        }
    }

    void PlayAudioAnimationEvent(string name) {
        FindObjectOfType<AudioManager>().PlaySoundAtLocation(name, transform.position);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(RangeOffset.x, RangeOffset.y, 0f), DetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(RangeOffset.x, RangeOffset.y, 0f), AtkRange);
        Gizmos.DrawCube(transform.position + (transform.right * HitboxOffset.x) + (transform.up * HitboxOffset.y), HitboxSize);
    }


}
