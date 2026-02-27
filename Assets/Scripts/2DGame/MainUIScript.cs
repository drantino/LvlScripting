using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] TwoDGameState gameState;
    public GameObject noSaveDataText, mainMenuUI;
    public void StartNewGame()
    {
        gameState.StartNewGame();
        mainMenuUI.SetActive(false);
    }
    public void ContinueGame()
    {
        if(gameState.LoadSaveGame())
        {
            Debug.Log("asdf");
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
}
