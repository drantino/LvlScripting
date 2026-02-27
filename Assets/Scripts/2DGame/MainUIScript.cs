using UnityEngine;
using UnityEngine.UI;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] TwoDGameState gameState;
    public GameObject noSaveDataText, mainMenuUI;
    public Image treasureChest0, treasureChest1, treasureChest2;
    public void StartNewGame()
    {
        gameState.StartNewGame();
        mainMenuUI.SetActive(false);
    }
    public void ContinueGame()
    {
        if(gameState.LoadSaveGame())
        {
            mainMenuUI.SetActive(false);
        }
        else
        {
            noSaveDataText.SetActive(true);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void TreasureUIGet(int index)
    {
        switch(index)
        {
            case 0:
                {
                    Color color = treasureChest0.color;
                    color.a = 1;
                    treasureChest0.color = color;
                    break;
                }
            case 1:
                {
                    Color color = treasureChest1.color;
                    color.a = 1;
                    treasureChest1.color = color;
                    break;
                }
            case 2:
                {
                    Color color = treasureChest2.color;
                    color.a = 1;
                    treasureChest2.color = color;
                    break;
                }
        }
    }
    public void ResetChestsUI()
    {
        Color color = treasureChest0.color;
        color.a = 0.43f;
        treasureChest0.color = color;
        treasureChest1.color = color;
        treasureChest2.color = color;
    }
}
