using UnityEngine;
using System.IO;

public class JSONSaving : MonoBehaviour
{
    string filePath;
    [ContextMenu("JSON save")]
    public void SaveData()
    {
        string file = "Assets/Resources/save.json";
        TwoDGameState.Instance.SaveDataUpdate();
        string json = JsonUtility.ToJson(TwoDGameState.Instance.saveData);
        File.WriteAllText(file, json);
        Debug.Log("Saved");
    }
    [ContextMenu("JSON Load")]
    public void LoadData()
    {
        string file = "Assets/Resources/save.json";
        if(File.Exists(file))
        {
            string json = File.ReadAllText(file);

            TwoDGameState.Instance.saveData = JsonUtility.FromJson<SaveData2D>(json);
            TwoDGameState.Instance.LoadSaveDate();
            Debug.Log("Loaded");
        }
        else
        {
            Debug.LogError("Save file not found.");
        }
    }
}
