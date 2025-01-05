using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffTemplate : MonoBehaviour
{   
    public int id;
    public float manaCost;
    public float coolDownTime;
    
    void Start()
    {
        ActivateBuff();
    }

    public virtual void ActivateBuff() {
        
    }
}

