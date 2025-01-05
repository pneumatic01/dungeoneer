using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    public static AudioManager instance;


    void Awake()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
            s.source.loop = s.looping;
            
        }
    }

    public void PlaySound(string name) {
        Sound sfx = Array.Find(sounds, sound => sound.name == name);
        if(sfx == null) { Debug.LogWarning($"Sound: {name} not found!"); }
        sfx.source.Play();
    }

    public void StopSound(string name) {
        Sound sfx = Array.Find(sounds, sound => sound.name == name);
        if(sfx == null) { Debug.LogWarning($"Sound: {name} not found!"); }
        sfx.source.Stop();
    }
    
    public void PlaySoundAtLocation(string name, Vector3 location)
    {
        Sound sfx = Array.Find(sounds, sound => sound.name == name);
        if (sfx == null) {
            Debug.LogWarning("Sound: " + name + " does not exist!");
            return;
        }

        CreateClipAtPoint(sfx.source, location);
    }

    public string RandomSound(string name, int amount) {
        int randomNum = UnityEngine.Random.Range(1, amount);
        return name + randomNum.ToString();
    }

    void CreateClipAtPoint(AudioSource source, Vector3 location) {
        GameObject tempAudio = new GameObject("OneShotAudio");
        tempAudio.transform.position = location;
        AudioSource _source = tempAudio.AddComponent<AudioSource>();
        _source.clip = source.clip;
        _source.volume = source.volume;
        _source.pitch = source.pitch;
        _source.spatialBlend = source.spatialBlend;
        _source.loop = source.loop;
        _source.Play();
        Destroy(tempAudio, _source.clip.length);
    }


}
