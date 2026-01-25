using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacingMainMenuScript : MonoBehaviour
{
    GameState gameState;

    public GameObject sliderR, sliderG, sliderB;
    public TextMeshProUGUI txtR, txtG, txtB, profileDeleteTxt, recordTimeTxtValue;
    public GameObject loadProfilePanel, deleteProfilePanel, deleteProfileConfirmPanel, vehicleSelectPanel;
    public TMP_Dropdown profileDropDown, profileDeleteDropDown;
    public TMP_InputField profileNameInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        UpdateUI();
    }

    public void StartGame()
    {
        vehicleSelectPanel.SetActive(true);
    }
    public void StartRace()
    {
        gameState.currentGameState = GameState.RacingGameState.PreRace;
        SceneManager.LoadScene("SampleScene");
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void VehicleTypeSelect(int typeID)
    {
        gameState.currentProfile.vehicleType = typeID;
        //update preview
    }
    public void RGBSelectUpdate(int RGBID)
    {
        //RGBID 0 = Red, 1 = Green, 2 = Blue
        switch (RGBID)
        {
            case 0:
                {
                    txtR.text = "R:" + sliderR.GetComponent<Slider>().value.ToString();
                    gameState.currentProfile.vehicleColorR = sliderR.GetComponent<Slider>().value;
                    break;
                }
            case 1:
                {
                    txtG.text = "G:" + sliderG.GetComponent<Slider>().value.ToString();
                    gameState.currentProfile.vehicleColorG = sliderG.GetComponent<Slider>().value;
                    break;
                }
            case 2:
                {
                    txtB.text = "B:" + sliderB.GetComponent<Slider>().value.ToString();
                    gameState.currentProfile.vehicleColorB = sliderB.GetComponent<Slider>().value;
                    break;
                }
            default:
                {
                    Debug.Log("Invalid RGBID");
                    break;
                }
        }
    }
    public void LoadProfilePanel()
    {
        gameState.LoadExistingProfiles();
        if (gameState.profileList.Count > 0)
        {
            loadProfilePanel.SetActive(true);
            //fill dropdown with profile names
            profileDropDown.ClearOptions();
            List<TMP_Dropdown.OptionData> data = new List<TMP_Dropdown.OptionData>();
            for (int index = 0; index < gameState.profileList.Count; index++)
            {
                TMP_Dropdown.OptionData newData = new TMP_Dropdown.OptionData();
                newData.text = gameState.profileList[index].profileName.ToString();
                data.Add(newData);
            }
            profileDropDown.AddOptions(data);
        }
        else
        {
            Debug.Log("No profiles to load");
        }

    }
    public void ConfirmLoadProfile()
    {
        if (profileDropDown.value <= gameState.profileList.Count)
        {
            gameState.SetToProfile(profileDropDown.value);
            UpdateUI();
            Debug.Log("Profile Set");
        }

    }
    public void CancelLoadProfile()
    {
        loadProfilePanel.SetActive(false);
    }
    public void LoadDeleteProfilePanel()
    {
        gameState.LoadExistingProfiles();
        if (gameState.profileList.Count > 0)
        {

            deleteProfilePanel.SetActive(true);
            //fill dropdown with profile names
            profileDeleteDropDown.ClearOptions();
            List<TMP_Dropdown.OptionData> data = new List<TMP_Dropdown.OptionData>();
            for (int index = 0; index < gameState.profileList.Count; index++)
            {
                TMP_Dropdown.OptionData newData = new TMP_Dropdown.OptionData();
                newData.text = gameState.profileList[index].profileName.ToString();
                data.Add(newData);
            }
            profileDeleteDropDown.AddOptions(data);
        }
        else
        {
            Debug.Log("No profiles to load");
        }
    }
    public void CancelDeleteProfile()
    {
        deleteProfilePanel.SetActive(false);
        deleteProfileConfirmPanel.SetActive(false);
    }
    public void InitiateDeleteProfile()
    {
        deleteProfileConfirmPanel.SetActive(true);
        profileDeleteTxt.text = $"Delete {gameState.profileList[profileDeleteDropDown.value].profileName}?";
    }
    public void ConfirmDeleteProfile()
    {
        gameState.profileList.Remove(gameState.profileList[profileDeleteDropDown.value]);
        CancelDeleteProfile();//its already there so use it
        gameState.UpdateProfileSaveFile();
    }
    public void UpdateProfileName()
    {
        gameState.currentProfile.profileName = profileNameInputField.text;
    }
    public void UpdateUI()
    {
        //update profile name
        profileNameInputField.text = gameState.currentProfile.profileName;
        //update vehicle type

        //update RGB select
        sliderR.GetComponent<Slider>().value = gameState.currentProfile.vehicleColorR;
        txtR.text = "R:" + sliderR.GetComponent<Slider>().value.ToString();
        sliderG.GetComponent<Slider>().value = gameState.currentProfile.vehicleColorG;
        txtG.text = "G:" + sliderG.GetComponent<Slider>().value.ToString();
        sliderB.GetComponent<Slider>().value = gameState.currentProfile.vehicleColorB;
        txtB.text = "B:" + sliderB.GetComponent<Slider>().value.ToString();
        //Update record time
        recordTimeTxtValue.text = (gameState.currentProfile.recordTime/60 - (gameState.currentProfile.recordTime / 60 % 1)).ToString()+":"+(gameState.currentProfile.recordTime%60).ToString();
    }
}
