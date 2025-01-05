using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{

    void Start() {
        ParticleSystem emitter = transform.GetChild(0).GetComponent<ParticleSystem>();
        var duration = emitter.main.duration;
        Destroy(gameObject, duration);
    }

}
