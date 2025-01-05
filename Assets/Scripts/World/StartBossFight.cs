using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{  
    public Sprite doorClosed;
    public GameObject EntryDoor;
    public GameObject Boss;
    public GameObject BossHPBar;
    public Transform SpawnPoint;


    void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<AudioManager>().PlaySound("musicBossFight");
        EntryDoor.GetComponent<SpriteRenderer>().sprite = doorClosed;
        EntryDoor.GetComponent<BoxCollider2D>().enabled = true;
        Instantiate(Boss, SpawnPoint.position, Quaternion.identity);
        BossHPBar.SetActive(true);
        Destroy(gameObject);
    }   
}
