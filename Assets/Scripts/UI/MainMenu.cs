using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject parent;
    Player player;

    void Start() {
        parent = transform.parent.gameObject;
        player = FindObjectOfType<Player>();

        Application.targetFrameRate = 120;
        SetupAudio();
    }

    public void InstantiateMenu(GameObject obj) {
        Instantiate(obj, parent.transform);
    }

    void SetupAudio() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        AudioSource[] sounds = audioManager.GetComponents<AudioSource>();
        foreach(AudioSource s in sounds) {
            if(s.clip != null && !s.clip.name.Contains("music")) {
                s.volume = 1;
            }
        
        }
    }

    



}
