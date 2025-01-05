using System.Collections;
using UnityEngine;

public class Healing : BuffTemplate
{

    public float iterations;
    public float delay;
    public float healPerTick = 2;

    public override void ActivateBuff()
    {
        Player player = transform.parent.GetComponent<Player>();
        healPerTick += player.castLevel - 1;
        StartCoroutine(StartBuff());
    }

    IEnumerator StartBuff() {
        for(int i = 0; i < iterations; i++) {
            transform.parent.GetComponent<Entity>().RestoreHealth(healPerTick);
            yield return new WaitForSeconds(delay);
        }

        Destroy(gameObject);
    }
}
