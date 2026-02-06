using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public RacingGameState currentGameState;
    public Profile currentProfile;
    public List<Profile> profileList;

    public string filePath;
    public enum RacingGameState
    {
        MainMenu,
        GamePaused,
        Racing,
        PreRace,
        PostRace
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
            //currentProfile = new Profile(0, "Default", 1, 1, 1, 0, 1);
    }
    public void LoadExistingProfiles()
    {
        profileList.Clear();
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                try
                {
                    profileList.Add(new Profile(int.Parse(columns[0]),columns[1], int.Parse(columns[2]), float.Parse(columns[3]), float.Parse(columns[4]), float.Parse(columns[5]), float.Parse(columns[6])));
                }
                catch
                {
                    Debug.Log($"Failed to Load Profile {i + 1}");
                }
            }
        }
    }
    public void SaveProfile()
    {
        LoadExistingProfiles();
        if(profileList.Count == 0)
        {
            //if no existing data write new
            bool fileExists = File.Exists(filePath);
            using(StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("ProfileID,ProfileName,VehicleType,VehicleColorR,VehicleColorG,VheicleColorB,RecordTime");
                writer.WriteLine($"0,{currentProfile.profileName},{currentProfile.vehicleType},{currentProfile.vehicleColorR},{currentProfile.vehicleColorG},{currentProfile.vehicleColorB},{currentProfile.recordTime}");
            }
            Debug.Log("Created file and profile");
        }
        else
        {
            //find existing profile name and adjust that profile, write it
            bool existingProfile = false;
            for (int index = 0; index < profileList.Count; index++)
            {
                if (profileList[index].profileName == currentProfile.profileName)
                {
                    existingProfile = true;
                    profileList[index].profileName = currentProfile.profileName;
                    profileList[index].vehicleType = currentProfile.vehicleType;
                    profileList[index].vehicleColorR = currentProfile.vehicleColorR;
                    profileList[index].vehicleColorG = currentProfile.vehicleColorG;
                    profileList[index].vehicleColorB = currentProfile.vehicleColorB;
                    profileList[index].recordTime = currentProfile.recordTime;

                    index = profileList.Count;
                    bool fileExists = File.Exists(filePath);
                    using (StreamWriter writer = new StreamWriter(filePath, false))
                    {
                        writer.WriteLine("ProfileID,ProfileName,VehicleType,VehicleColorR,VehicleColorG,VheicleColorB,RecordTime");
                        for(int reindex = 0; reindex < profileList.Count; reindex++)
                        {
                            writer.WriteLine($"{reindex},{profileList[reindex].profileName},{profileList[reindex].vehicleType},{profileList[reindex].vehicleColorR},{profileList[reindex].vehicleColorG},{profileList[reindex].vehicleColorB},{profileList[reindex].recordTime}");
                        }
                    }
                    Debug.Log("Updated Profile");
                }
            }
            //if no duplicate profile, add new profile and write it
            if(!existingProfile)
            {
                bool fileExists = File.Exists(filePath);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{profileList.Count},{currentProfile.profileName},{currentProfile.vehicleType},{currentProfile.vehicleColorR},{currentProfile.vehicleColorG},{currentProfile.vehicleColorB},{currentProfile.recordTime}");
                }
                Debug.Log("New Profile created");
            }
        }
    }
    public void UpdateProfileSaveFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("ProfileID,ProfileName,VehicleType,VehicleColorR,VehicleColorG,VheicleColorB,RecordTime");
            for (int reindex = 0; reindex < profileList.Count; reindex++)
            {
                writer.WriteLine($"{reindex},{profileList[reindex].profileName},{profileList[reindex].vehicleType},{profileList[reindex].vehicleColorR},{profileList[reindex].vehicleColorG},{profileList[reindex].vehicleColorB},{profileList[reindex].recordTime}");
            }
        }
    }
    public void SetToProfile(int profileID)
    {
        currentProfile.profileID = profileList[profileID].profileID;
        currentProfile.profileName = profileList[profileID].profileName;
        currentProfile.vehicleType = profileList[profileID].vehicleType;
        currentProfile.vehicleColorR = profileList[profileID].vehicleColorR;
        currentProfile.vehicleColorG = profileList[profileID].vehicleColorG;
        currentProfile.vehicleColorB = profileList[profileID].vehicleColorB;
        currentProfile.recordTime = profileList[profileID].recordTime;
    }
}
[Serializable]
public class Profile
{
    public int profileID;
    public string profileName;
    public int vehicleType;//might change to enum
    public float vehicleColorR, vehicleColorG, vehicleColorB;
    public float recordTime;
    public Profile(int _profileID,string _profileName, int _vehicleType, float _vehicleColorR, float _vehicleColorG, float _vehicleColorB, float _recordTime)
    {
        profileID = _profileID;
        profileName = _profileName;
        vehicleType = _vehicleType;
        vehicleColorR = _vehicleColorR;
        vehicleColorG = _vehicleColorG;
        vehicleColorB = _vehicleColorB;
        recordTime = _recordTime;
    }
}
