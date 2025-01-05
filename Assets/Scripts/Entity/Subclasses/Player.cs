using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static event Action onPlayerDeath;

    private bool _isGrounded;
    private float _direction;
    [HideInInspector] public Vector2 inputRaw;
    [HideInInspector] public float damageMultiplier = 1;
    private float currentMana;
    private int aetherEssence = 0;

    [Header("Player Stats")]
    public float maxMana;
    public float manaRecharge;
    public int hpLevel;
    public int manaLevel;
    public int castLevel;


    [Header("References")]
    public Transform groundChecker;
    public LayerMask groundLayer;
    public GameObject deathEffect;

    [Header("Movement Settings")]
    public float moveSpeed;
    public float jumpPower;
    public float groundCheckRange;
    private float _lastCastTime = 0;


    [Header("Inventory")]
    public GameObject baseWeapon;
    public GameObject PrimarySpell;
    public GameObject SecondarySpell;

    private bool primaryCooldown = false;
    private bool secondaryCooldown = false;

    [HideInInspector] public int levelsUnlocked = 1;


    void Start() {
        maxMana *= manaLevel;
        manaRecharge *= manaLevel;
        maxHealth = maxHealth + (20 * (hpLevel - 1));
        _currentHealth = maxHealth;
        currentMana = maxMana;
        Mathf.Clamp(currentMana, 0f, maxMana);
    }

    void Update() {
        _lastCastTime += Time.deltaTime;
        setCurrentMana(currentMana += Time.deltaTime * manaRecharge) ;
    }

    public int GetAetherEssence() {
        return aetherEssence;
    }

    public void AddAetherEssence(int n) {
        aetherEssence += n;
    }

    public void SetAetherEssence(int n) {
        aetherEssence = n;
    }

    public float getMaxMana() {
        return maxMana;
    }

    public float getCurrentMana() {
        return currentMana;
    }

    public void setMaxMana(float n) {
        maxMana = n;
    }

    public void setCurrentMana(float n) {
        currentMana = n;
        if(currentMana < 0) { currentMana = 0; }
        if(currentMana > maxMana) { currentMana = maxMana; }
    }
    

    public void Locomotion(Vector2 input) {
        _isGrounded = Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckRange, groundLayer);
        _direction = input.x == 0 ? 0 : Mathf.Sign(input.x);
        inputRaw = input;

        if(input.y > 0.7f || input.y < -0.7f) {
            _direction = 0;
        }

        Vector2 velocity = new Vector2(_direction * moveSpeed, rb.velocity.y);
        rb.velocity = velocity;

        FlipPlayerSprite();
        Animate();
    }

    private bool CanShoot() => _lastCastTime > 1f / (baseWeapon.GetComponent<Projectile>().fireRate / 60);

    public void BaseAttack(Vector2 input) {

        Debug.DrawRay(transform.position, input.normalized);
        
        Vector2 dirNormal = input.normalized;
        Vector3 offset = new Vector3(dirNormal.x, dirNormal.y, 0); 
        float angle = Mathf.Atan2(dirNormal.y, dirNormal.x) * Mathf.Rad2Deg + 90; // rotates in the proper direction

        if(dirNormal != Vector2.zero && CanShoot()) {
            Projectile proj = baseWeapon.GetComponent<Projectile>();
            float bulletAngle = UnityEngine.Random.Range(angle + -proj.bulletSpread, angle + proj.bulletSpread);
            GameObject bullet = Instantiate(baseWeapon, transform.position + offset, Quaternion.Euler(0, 0, bulletAngle));
            bullet.GetComponent<Projectile>().damage *= damageMultiplier;

            _lastCastTime = 0;
        }     
    }

    public void CastPrimarySpell() {
        if(!primaryCooldown) {
            BuffTemplate spell = PrimarySpell.GetComponent<BuffTemplate>();
            if((currentMana - spell.manaCost) <= 0) { return; }
            GameObject spawnedSpell = Instantiate(PrimarySpell, gameObject.transform);

            setCurrentMana(currentMana - spell.manaCost);
            StartCoroutine(Cooldown(() => primaryCooldown = true, () => primaryCooldown = false, spell.coolDownTime));
        }
    } 

    public void CastSecondarySpell() {
        if(!secondaryCooldown) {
            BuffTemplate spell = SecondarySpell.GetComponent<BuffTemplate>();
            if((currentMana - spell.manaCost) <= 0) { return; }

            GameObject spawnedSpell = Instantiate(SecondarySpell, gameObject.transform);
            setCurrentMana(currentMana - spell.manaCost);
            StartCoroutine(Cooldown(() => secondaryCooldown = true, () => secondaryCooldown = false, spell.coolDownTime));
        }
    }


    void FlipPlayerSprite() {
        if(_direction > 0) { transform.rotation = Quaternion.Euler(0, 0, 0); }
        if(_direction < 0) { transform.rotation = Quaternion.Euler(0, 180, 0); }  
    }

    void Animate() {
        animator.SetFloat("Speed", Mathf.Abs(_direction));
        animator.SetBool("Grounded", _isGrounded);
    }

    void PlayAudio() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        string soundToPlay = audioManager.RandomSound("footstep", 8);
        audioManager.PlaySoundAtLocation(soundToPlay, transform.position);
    }


    public void Jump() {
        if(_isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    public override void OnDeath()
    {
        onPlayerDeath?.Invoke();
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        base.OnDeath();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(groundChecker.position, Vector2.down * groundCheckRange);
    }

    IEnumerator Cooldown(Action onStart, Action onEnd, float time) {
        onStart?.Invoke();
        yield return new WaitForSeconds(time);
        onEnd?.Invoke();
    }

}
