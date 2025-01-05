
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("References")]
    public Player plr;
    public Image HPBar;
    public Image ManaBar;
    public TextMeshProUGUI aetherAmount;

    
    void Update()
    {
        aetherAmount.text = plr.GetAetherEssence().ToString();
        UpdateBars();
    }

    void UpdateBars() {
        float hfraction = plr.getCurrentHealth() / plr.getMaxHealth();
        float mFraction = plr.getCurrentMana() / plr.getMaxMana();
        HPBar.fillAmount = hfraction;
        ManaBar.fillAmount = mFraction;
    }
}
