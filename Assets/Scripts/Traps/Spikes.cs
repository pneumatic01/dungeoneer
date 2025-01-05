using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public int damage;

    void OnTriggerStay2D(Collider2D other) {
        Vector2 targetPos = other.transform.position;
        Vector2 myPos = transform.position;
        Vector2 direction = (myPos - targetPos).normalized;
        
        Entity entity = other.gameObject.GetComponent<Entity>();
        if(entity != null && !entity.isVulnerable) {
            entity.Damage(damage, -direction);
        }
        
    }
}
