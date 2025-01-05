using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameManager gameManager;
    private TextMeshPro msg;

    void Start() {
        msg = transform.Find("message").GetComponent<TextMeshPro>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(EnemiesLeft() <= 0) { SaveAndQuit(); }
        if(EnemiesLeft() > 0) { StartCoroutine(DeniedMessage()); }
    }

    int EnemiesLeft() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        return enemies.Length;
    }

    void SaveAndQuit() {
        Scene currentScene = SceneManager.GetActiveScene();
        if(gameManager.player.levelsUnlocked <= currentScene.buildIndex){
            gameManager.player.levelsUnlocked += 1;
        }

        gameManager.SavePlayer();
        SceneManager.LoadScene(0);
    }

    IEnumerator DeniedMessage() {
        msg.text = "Kill all enemies to proceed";
        yield return new WaitForSeconds(2f);
        msg.text = string.Empty;
    }

}
