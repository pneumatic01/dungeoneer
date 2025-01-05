using System;
using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    public static event Action onBossDeath;
    [HideInInspector] public float spinSpeed = 0;
    [HideInInspector] public bool leaping = false;
    private float fadeDuration = 1f;
    

    public override void OnDeath()
    {
        animator.Play("Dead");
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CapsuleCollider2D>());
        onBossDeath?.Invoke();

        StartCoroutine(DecayBody());
        StartCoroutine(spawnDrops());

    }

    IEnumerator spawnDrops() {
        int amount = UnityEngine.Random.Range(aetherDropMin, aetherDropMax);
        for(int i = 0; i < amount; i++) {
            yield return new WaitForSeconds(0.05f);
            Instantiate(aetherEssence, transform.position, Quaternion.identity);
        }
    }

    IEnumerator DecayBody() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        yield return new WaitForSeconds(4f);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        Destroy(gameObject);
    }


    // for spin attack
    private Coroutine spinAttackCoroutine;

    public void SetSpinSpeed(float amount) {
        spinSpeed = amount;
    }

    public void spawnSpinAttackHitbox(int dmg) {
        if (spinAttackCoroutine == null)
            spinAttackCoroutine = StartCoroutine(SpinAttackHitbox(dmg));
    }

    public void stopSpinAttackHitbox() {
        if (spinAttackCoroutine != null)
        {
            StopCoroutine(spinAttackCoroutine);
            spinAttackCoroutine = null; 
        }   
    }

    IEnumerator SpinAttackHitbox(int dmg) {
        while(true) {
            Vector2 pos = transform.position;
            Vector2 size = new Vector2(2f, 2f);
            LayerMask playerMask = LayerMask.GetMask("Player");
            Collider2D hitbox = Physics2D.OverlapBox(pos, size, 0f, playerMask);
            if(hitbox && hitbox.GetComponent<Player>()) {
                hitbox.GetComponent<Player>().Damage(dmg, Vector2.up);
                break;
            }
            yield return null; 
        }
        
    }

    //Leap attack stuff

    public void ActivateLeapAttack(int condition) {
        if(condition == 1) {
            leaping = true;
            rb.gravityScale = 0f; 
        }

        if(condition == 0) { 
            leaping = false;
            rb.gravityScale = 5f; 
        }
    }

    public void LeapAttackTP() {
        if(facingRight) { 
            transform.position += new Vector3(8.5f, -4, 0f);
        }
        if(!facingRight) {
            transform.position += new Vector3(-8.5f, -4, 0f);
        }
        
    }

    public void LeapHammerHitBox(int dmg) {
        Vector3 pos = Vector3.zero;
        if(facingRight) { 
            pos = transform.position + new Vector3(8.5f, -4, 0f);
        }
        if(!facingRight) {
            pos = transform.position + new Vector3(-8.5f, -4, 0f);
        }

        FindObjectOfType<AudioManager>().PlaySoundAtLocation("bossRockSlam", pos);
        Vector2 size = new Vector2(3f, 3f);
        LayerMask playerMask = LayerMask.GetMask("Player");
        Collider2D hitbox = Physics2D.OverlapBox(pos, size, 0f, playerMask);
        if(hitbox && hitbox.GetComponent<Player>()) {
            hitbox.GetComponent<Player>().Damage(dmg, Vector2.up);
        }
    }

    public void LeapSwordHitBox(int dmg) {
        Vector3 pos = Vector3.zero;
        if(facingRight) { 
            pos = transform.position + new Vector3(1f, 0f, 0f);
        }
        if(!facingRight) {
            pos = transform.position + new Vector3(-1f, 0, 0f);
        }

        Vector2 size = new Vector2(2f, 2f);
        LayerMask playerMask = LayerMask.GetMask("Player");
        Collider2D hitbox = Physics2D.OverlapBox(pos, size, 0f, playerMask);
        if(hitbox && hitbox.GetComponent<Player>()) {
            hitbox.GetComponent<Player>().Damage(dmg, Vector2.up);
        }
    }

}
