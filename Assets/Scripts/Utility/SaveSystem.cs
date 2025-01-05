using System.IO;
using UnityEngine;

public static class SaveSystem
{

    public static void SaveGame(Player player) {
        string path = Path.Combine(Application.persistentDataPath, "savegame.json");
        SaveData saveData = new SaveData(player);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public static SaveData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "savegame.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            return saveData;
        }


        Debug.LogWarning("No save file found!");
        return null;
    }

    // static void CreateNewSave() {
    //     SaveData saveData = new SaveData(player);
    // }
}
