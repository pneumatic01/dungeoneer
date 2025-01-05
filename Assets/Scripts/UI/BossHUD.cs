using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    [Header("References")]
    private Boss boss;
    public Image HPBar;


    void Update()
    {
        if(FindObjectOfType<Boss>() != null) {
            boss = FindObjectOfType<Boss>();
            float hfraction = boss.getCurrentHealth() / boss.getMaxHealth();
            HPBar.fillAmount = hfraction;
        }
    }
}
