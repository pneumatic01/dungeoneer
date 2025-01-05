using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int spell_id;
    public int buff_id;
    public int aetherEssence;

    public int healthLevel;
    public int manaLevel;
    public int castLevel;

    public int levelsUnlocked;

    public SaveData(Player player) {
        healthLevel = player.hpLevel;
        manaLevel = player.manaLevel;
        castLevel = player.castLevel;
        levelsUnlocked = player.levelsUnlocked;

        aetherEssence = player.GetAetherEssence();

        if(player.baseWeapon != null) {
            spell_id = player.baseWeapon.GetComponent<Projectile>().id;
        }

        if(player.PrimarySpell != null) {
            buff_id = player.PrimarySpell.GetComponent<BuffTemplate>().id;
        }
        
        
    }

}
