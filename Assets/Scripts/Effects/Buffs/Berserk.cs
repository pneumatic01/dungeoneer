using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : BuffTemplate
{
    public float duration;
    
    private float multiplier = 1.5f;
    private float originalMoveSpeed;
    private float originalJumpPower;
    private float originalDamageMultiplier;

    public override void ActivateBuff()
    {
        Player player = transform.parent.GetComponent<Player>();
        multiplier += player.castLevel * 0.25f;
        StartCoroutine(StartBerserk());
    }


    IEnumerator StartBerserk() {
        Player player = transform.parent.GetComponent<Player>();

        originalMoveSpeed = player.moveSpeed;
        originalJumpPower = player.jumpPower;
        originalDamageMultiplier = player.damageMultiplier;

        player.moveSpeed = originalMoveSpeed * multiplier;
        player.jumpPower = originalJumpPower * multiplier;
        player.damageMultiplier = originalDamageMultiplier * multiplier;

        yield return new WaitForSeconds(duration);

        player.moveSpeed = originalMoveSpeed;
        player.jumpPower = originalJumpPower;
        player.damageMultiplier = originalDamageMultiplier;

        Destroy(gameObject);
    }
}
