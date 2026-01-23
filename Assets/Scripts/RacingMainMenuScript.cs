using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacingMainMenuScript : MonoBehaviour
{
    GameState gameState;

    public GameObject sliderR, sliderG, sliderB;
    public TextMeshProUGUI txtR, txtG, txtB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    public void StartGame()
    {

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
                    txtR.text = "R:"+sliderR.GetComponent<Slider>().value.ToString();
                    gameState.currentProfile.vehicleColorR = sliderR.GetComponent<Slider>().value;
                    break;
                }
            case 1:
                {
                    break;
                }
            case 2:
                {

                    break;
                }
            default:
                {
                    Debug.Log("Invalid RGBID");
                    break;
                }
        }
    }
}
