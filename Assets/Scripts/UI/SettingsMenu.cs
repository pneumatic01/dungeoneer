using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TextMeshProUGUI sound;

    private bool soundOn = true;

    void Start() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();   
        AudioSource[] sounds = audioManager.GetComponents<AudioSource>();

        foreach(AudioSource s in sounds) {
            if(s.volume > 0) {
                sound.text = "Sound: On";
                soundOn = true;
            }
            if(s.volume == 0) {
                sound.text = "Sound: Off";
                soundOn = false;
            }
        }
    }

    public void ToggleSounds() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();   
        AudioSource[] sounds = audioManager.GetComponents<AudioSource>();

        if(soundOn) {
            foreach(AudioSource s in sounds) {
                if(s.clip != null && !s.clip.name.Contains("music")) {
                    s.volume = 1;
                }
                sound.text = "Sound: Off";
            }
        }

        if(!soundOn) {
            foreach(AudioSource s in sounds) {
                if(s.clip != null && !s.clip.name.Contains("music")) {
                    s.volume = 0;
                }
                sound.text = "Sound: On";
            }
        }

        soundOn = !soundOn;
    }
}
