using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AetherEssence : MonoBehaviour
{   
    public LayerMask plrMask;
    public float size;
    private Rigidbody2D _rigidbody;
    private float despawnTime = 60f;
    Collider2D hitbox;

    private float xForce = 6f;
    private float yForce = 6f;
    private Transform plr;
    private bool follow = false;
    

    void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        AddForce();
        Destroy(gameObject, despawnTime);
        plr = FindObjectOfType<Player>().transform;
    }

    void Update() {
        hitbox = Physics2D.OverlapCircle(transform.position, size, plrMask);
        if(hitbox && hitbox.gameObject.GetComponent<Player>() != null) {
            FindObjectOfType<AudioManager>().PlaySoundAtLocation("pickup", transform.position);
            hitbox.gameObject.GetComponent<Player>().AddAetherEssence(1);
            Destroy(gameObject);
        }

        if(follow) {
            Vector3 target = plr.position;
            transform.position = Vector2.MoveTowards(transform.position, target, 10f * Time.deltaTime);
        }
    }

    void AddForce() {
        float x = Random.Range(-xForce, xForce);
        float y = Random.Range(-yForce, yForce);
        Vector2 force = new Vector2(x, y);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer() {
        yield return new WaitForSeconds(1f);
        follow = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, size);
    }

}
