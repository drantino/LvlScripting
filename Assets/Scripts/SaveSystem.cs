using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    
    public string filePath;
    public List<SaveData> saveDataList = new List<SaveData>();
    private void Start()
    {
        //string[] lines =
        //{
        //    "Profile Name, Time",
        //    "Sujan, 2000",
        //    "Naruto, 3034"
        //};
        //File.WriteAllLines(filePath, lines);
        //Debug.Log("Saved");
        LoadData("Sujan");
    }
    public void CreateSave(string _profileName, int _highScore)
    { 
        SaveData saveData = new SaveData(_profileName, _highScore);
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if(!fileExists)
            {
                writer.WriteLine("ProfileName, Score");
            }
            writer.WriteLine($"{saveData.profileName}, {saveData.highScore}");

        }
    }
    public void UpdateSave(SaveData _saveData)
    {

    }
    public void DeleteSave(SaveData _saveData)
    {

    }
    public void LoadData(string profileName)
    {
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            if (columns[0] == profileName)
            {
                SaveData saveData = new SaveData(profileName, int.Parse(columns[1]));
                saveDataList.Add(saveData);
                i = lines.Length;
            }
            
        }
    }
}
[Serializable]
public class SaveData
{
    public string profileName;
    public int highScore;

    public SaveData(string _profileName, int _highScore)
    {
        profileName = _profileName;
        highScore = _highScore;
    }
}

