using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    Player player;

    void Start() {
        player = FindObjectOfType<Player>();
    }

    void OnCollisionStay2D(Collision2D other) {
        if(player != null) {
            if(player.inputRaw.y < -0.7f) {
                StartCoroutine(DisableCollision());
            }
        }
    } 

    IEnumerator DisableCollision() {
        Collider2D plrCollider = player.GetComponent<Collider2D>();
        EdgeCollider2D platformCollider = GetComponent<EdgeCollider2D>();
        
        Physics2D.IgnoreCollision(plrCollider, platformCollider, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(plrCollider, platformCollider, false);
    }
}
