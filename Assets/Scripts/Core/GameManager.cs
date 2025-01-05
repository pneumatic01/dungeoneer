using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] spellPrefabs;
    public GameObject[] buffPrefabs;
    public Player player;

    public GameObject playerDeathScreen;
    public Transform playerCanvas;

    void Start() {
        StartMusic();
        LoadGame();
        Player.onPlayerDeath += PlayerDeath;
    }

    public GameObject GetSpellById(int spellID) {
        foreach (GameObject prefab in spellPrefabs) {
            Projectile spell = prefab.GetComponent<Projectile>();
            if (spell != null && spell.id == spellID) {
                return prefab;
            }
        }
        return null;
    }

    public GameObject GetBuffById(int buffID) {
        foreach (GameObject prefab in buffPrefabs) {
            BuffTemplate buff = prefab.GetComponent<BuffTemplate>();
            if (buff != null && buff.id == buffID) {
                return prefab;
            }
        }
        return null;
    }

    public void SavePlayer() {
        SaveSystem.SaveGame(player);
    }

    public void LoadGame() {
        SaveData data = SaveSystem.LoadGame();
        if(data != null) {
            player.hpLevel = data.healthLevel;
            player.manaLevel = data.manaLevel;
            player.castLevel = data.castLevel;
            player.levelsUnlocked = data.levelsUnlocked;
            player.SetAetherEssence(data.aetherEssence);
            player.baseWeapon = GetSpellById(data.spell_id);
            player.PrimarySpell = GetBuffById(data.buff_id);
        }

    }

    void PlayerDeath() {
        Instantiate(playerDeathScreen, playerCanvas);
        Player.onPlayerDeath -= PlayerDeath;
    }

    void StartMusic() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if(sceneName == "MainMenu") {
            audioManager.PlaySound("musicMainMenu");
        }
        else {
            audioManager.StopSound("musicMainMenu");
        }


    }


}
