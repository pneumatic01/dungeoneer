using UnityEngine;
using System.Collections;


public class Entity : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    private bool invulnerable = false;
    protected float _currentHealth;
    private const float flashTime = 0.1f;

    protected float knockbackTime = 0.2f;
    protected float hitDirectionForce = 10f;
    protected float constForce = 5f;

    public bool isKnockedback { get; private set; }

    [Header("Settings")]
    public float maxHealth;
    public float iframeTime;
    

    public bool isVulnerable => !invulnerable;

    void Awake()
    {
        _currentHealth = maxHealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public float getMaxHealth() {
        return maxHealth;
    }

    public void setMaxHealth(float n) {
        maxHealth = n;
    }

    public float getCurrentHealth() {
        return _currentHealth;
    }

    public void Damage(float amount, Vector2 hitDirection) {
        if(amount <= 0 || !isVulnerable) { return; } 
        _currentHealth -= amount;
        if(_currentHealth <= 0) {
            OnDeath();
        }
        OnDamage(hitDirection);
    }

    void OnDamage(Vector2 hitDirection) {
        FindObjectOfType<AudioManager>().PlaySoundAtLocation("damage", transform.position);
        StartCoroutine(iFrame());
        StartCoroutine(HitFlash());
        //CallKnockback(hitDirection, Vector2.up / 2);
    }


    public void RestoreHealth(float amount) {
        _currentHealth += amount;
        if(_currentHealth > maxHealth) {
            _currentHealth = maxHealth;
        }
    }


    IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection) {
        isKnockedback = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;
        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < knockbackTime) {
            _elapsedTime += Time.fixedDeltaTime;
            _knockbackForce = _hitForce + _constantForce;
            _combinedForce = _knockbackForce;          
            rb.velocity = _combinedForce;
        
            yield return new WaitForFixedUpdate();
        }

        isKnockedback = false;
        
    } 


    public void CallKnockback(Vector2 hitDirection, Vector2 constantForceDirection) {
        StartCoroutine(KnockbackAction(hitDirection, constantForceDirection));
    }

    public virtual void OnDeath() {
        Destroy(gameObject);
    }

    

    IEnumerator iFrame() {
        invulnerable = true;
        yield return new WaitForSeconds(iframeTime);
        invulnerable = false;
    }

    IEnumerator HitFlash() {
        gameObject.GetComponent<SpriteRenderer>().material.SetInt("_Hit", 1);
        yield return new WaitForSeconds(flashTime);
        gameObject.GetComponent<SpriteRenderer>().material.SetInt("_Hit", 0);
    }


}
