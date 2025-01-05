using UnityEngine;

public class Ladder : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) {
        if(other.GetComponent<Player>() != null) {
            Player plr = other.GetComponent<Player>();
            if(plr.inputRaw.y > 0.7f) {
                Vector2 climbVelocity = new Vector2(0f, plr.jumpPower);
                plr.rb.velocity = climbVelocity;
            }
        }
    }
}
