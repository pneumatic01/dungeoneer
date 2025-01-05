
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    Player player;
    GameManager gameManager;

    private GameObject MainMenu;
    public TextMeshProUGUI aetherDisplay;
    public Transform HPUpgrade;
    public Transform ManaUpgrade;
    public Transform CastUpgrade;

    int hpCost;
    int manaCost;
    int castCost;
    

    void Start()
    {
        MainMenu = FindObjectOfType<MainMenu>().gameObject;
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        LoadValues();
    }

    void LoadValues() {
        aetherDisplay.text = player.GetAetherEssence().ToString();

        HPUpgrade.Find("level").GetComponent<TextMeshProUGUI>().text = $"Level: {player.hpLevel}";
        ManaUpgrade.Find("level").GetComponent<TextMeshProUGUI>().text = $"Level: {player.manaLevel}";
        CastUpgrade.Find("level").GetComponent<TextMeshProUGUI>().text = $"Level: {player.castLevel}";

        hpCost = 20 * (int)Mathf.Pow(2, player.hpLevel - 1);
        manaCost = 15 * (int)Mathf.Pow(2, player.manaLevel - 1);
        castCost = 30 * (int)Mathf.Pow(2, player.castLevel - 1);

        HPUpgrade.Find("cost").GetComponent<TextMeshProUGUI>().text = $"Cost: {hpCost}";
        ManaUpgrade.Find("cost").GetComponent<TextMeshProUGUI>().text = $"Cost: {manaCost}";
        CastUpgrade.Find("cost").GetComponent<TextMeshProUGUI>().text = $"Cost: {castCost}";


    }

    public void UpgradeHealth() {
        int currentAether = player.GetAetherEssence();
        if(currentAether > hpCost) {
            player.hpLevel += 1;
            player.SetAetherEssence(player.GetAetherEssence() - hpCost);
            gameManager.SavePlayer();
        }
        else {
            // just play a deny sound
        } 

        LoadValues();
    }

    public void UpgradeMana() {
        int currentAether = player.GetAetherEssence();
        if(currentAether > manaCost) {
            player.manaLevel += 1;
            player.SetAetherEssence(player.GetAetherEssence() - manaCost);
            gameManager.SavePlayer();
        }
        else {
            // just play a deny sound
        } 

        LoadValues();
    }

    public void UpgradeCast() {
        int currentAether = player.GetAetherEssence();
        if(currentAether > castCost) {
            player.castLevel += 1;
            player.SetAetherEssence(player.GetAetherEssence() - castCost);
            gameManager.SavePlayer();
        }
        else {
            // just play a deny sound
        } 

        LoadValues();
    }

    public void NextPage(GameObject obj) {
        Transform canvas = MainMenu.transform.parent;
        Instantiate(obj, canvas);
    }
}
