using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Sprite openSprite;
    public GameObject drop;
    public int amountMin;
    public int amountMax;
    

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<Player>() != null) {
            gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
            int dropAmount = Random.Range(amountMin, amountMax);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            StartCoroutine(spawnDrops(dropAmount));
        }
    }

    IEnumerator spawnDrops(int amount) {
        for(int i = 0; i < amount; i++) {
            yield return new WaitForSeconds(0.05f);
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
