using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    
    public string filePath;//Path = Assets/Resources/ProfilesData.csv 
    public List<SaveData> saveDataList = new List<SaveData>();
    private void Start()
    {
        
    }
    public void CreateSave(string _profileName, int _timeRecord)
    { 
        SaveData saveData = new SaveData(_profileName, _timeRecord);
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if(!fileExists)
            {
                writer.WriteLine("ProfileName, Time");
            }
            writer.WriteLine($"{saveData.profileName}, {saveData.timeRecord}");

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
        if(File.Exists(filePath))
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
}
[Serializable]
public class SaveData
{
    public string profileName;
    public int timeRecord;

    public SaveData(string _profileName, int _timeRecord)
    {
        profileName = _profileName;
        timeRecord = _timeRecord;
    }
}

