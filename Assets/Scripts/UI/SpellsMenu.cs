using UnityEngine;
using UnityEngine.UI;

public class SpellsMenu : MonoBehaviour
{
    public GameObject EquipBorder;
    GameManager gameManager;


    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void EquipSpell(GameObject spell) {
        setRectTransform(spell);

        int spellid = spell.name[0] - '0';
        gameManager.player.baseWeapon = gameManager.GetSpellById(spellid);
        gameManager.SavePlayer();
    }

    void setRectTransform(GameObject parent) {
        Image image = EquipBorder.GetComponent<Image>();
        Color color = image.color;
        color.a = 1f;
        image.color = color;

        EquipBorder.transform.SetParent(parent.transform);
        RectTransform rectTransform = EquipBorder.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }

}
