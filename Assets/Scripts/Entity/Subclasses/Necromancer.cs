using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    [Header("Necromancer")]
    public GameObject spell;
    private Transform castPoint;

    public override void Start() {
        base.Start();
        castPoint = transform.Find("castPoint").transform;
    }

    public void FireProjectile() {
        Player plr = FindAnyObjectByType<Player>();
        Vector2 _direction = plr.transform.position - castPoint.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        Projectile proj = spell.GetComponent<Projectile>();
        GameObject bullet = Instantiate(spell, castPoint.position, Quaternion.Euler(0f, 0f, angle + 90f));
    }

    float setAngle() {
        if(!facingRight) {
            return 120f;
        }
        if(facingRight) {
            return 60f;
        }

        return 0f;
    }

}

