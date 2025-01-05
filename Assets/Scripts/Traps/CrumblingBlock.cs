using System.Collections;
using UnityEngine;

public class CrumblingBlock : MonoBehaviour
{

    public GameObject smokeVFX;
    public GameObject crumbleEffect;
    public float crumbleTime;
    public float resetTime;
    private BoxCollider2D col;
    private SpriteRenderer spriteRenderer;
    private bool Activated = false;

    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<Entity>() != null && !Activated) {
            Activated = true;
            Instantiate(smokeVFX, transform.position, Quaternion.identity);
            StartCoroutine(Crumble());
        }
    }

    IEnumerator Crumble() {
        yield return new WaitForSeconds(crumbleTime);
        col.enabled = false;
        ChangeAlpha(0f);
        Instantiate(crumbleEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(resetTime);
        ChangeAlpha(1f);
        col.enabled = true;
        Activated = false;
        
    }

    void ChangeAlpha(float amount) {
        Color color = spriteRenderer.color;
        color.a = amount;
        spriteRenderer.color = color;
    }
}
