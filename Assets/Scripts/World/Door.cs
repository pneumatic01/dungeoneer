using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openSprite;
    void Start() {
        Boss.onBossDeath += Open;
    }

    void Open() {
        FindObjectOfType<AudioManager>().StopSound("musicBossFight");
        GetComponent<SpriteRenderer>().sprite = openSprite;
        GetComponent<BoxCollider2D>().enabled = false;
        Boss.onBossDeath -= Open;
    }
}
