using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageAmount;
    public Vector2 size;
    private Collider2D[] hitbox;


    void Update()
    {
        hitbox = Physics2D.OverlapBoxAll(transform.position, size, 0f);
        foreach(Collider2D obj in hitbox) {
            if(hitbox != null && obj.gameObject.GetComponent<Entity>() != null) {
                obj.gameObject.GetComponent<Entity>().Damage(damageAmount, Vector2.up);
            } 
        }

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
