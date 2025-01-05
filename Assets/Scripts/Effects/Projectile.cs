
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rb;
    
    [HideInInspector] public GameObject bulletOwner;
    public GameObject impactEffect;

    public int id;
    public float damage;
    public float speed;
    public float fireRate;
    public float lifeTime;
    public float bulletSpread;

    public string impactSound;
    public string fireSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        PlaySound(fireSound);
    } 

    void Update()
    {
        rb.velocity = -transform.up * speed;
        
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0) {
            Destroy(gameObject);
        }
    }

    public void SetOwner(GameObject owner) { //useless
        bulletOwner = owner;
        Collider2D self = gameObject.GetComponent<Collider2D>();
        Collider2D other = owner.gameObject.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(other, self, true);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(impactEffect != null) {  
            Instantiate(impactEffect, transform.position, Quaternion.identity); 
        }

        Entity enemy = other.gameObject.GetComponent<Entity>();
        if(enemy != null) {
            Vector2 targetPos = other.transform.position;
            Vector2 myPos = transform.position;
            Vector2 direction = (myPos - targetPos).normalized;

            enemy.Damage(damage, -direction);
        }

        PlaySound(impactSound);
        DetachParticles();
        Destroy(gameObject);  
    }

    void DetachParticles() {
        if(transform.childCount == 0) { return; }
        ParticleSystem emitter = transform.Find("Effect").GetComponent<ParticleSystem>();
        emitter.transform.parent = null;

        var emission = emitter.emission;
        emission.enabled = false;

        emitter.Stop();
        var main = emitter.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
    }

    public IEnumerator EnableColliderNextFrame() {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) {
            yield return null;
            collider.enabled = true;
        }
    }

    void PlaySound(string name) {
        if(name == string.Empty) { return; }
        FindObjectOfType<AudioManager>().PlaySoundAtLocation(name, transform.position);
    }
}
