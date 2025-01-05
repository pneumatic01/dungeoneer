using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI errorMsg;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadLevel(int index) {
        if(index <= gameManager.player.levelsUnlocked) {
           SceneManager.LoadScene(index); 
        }
        else {
            StartCoroutine(DeniedMessage());
        }
    }

    IEnumerator DeniedMessage() {
        errorMsg.text = "Level not unlocked!";
        yield return new WaitForSeconds(1f);
        errorMsg.text = string.Empty;
    }
}
