using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] TwoDGameState gameState;
    public static MainUIScript instance;
    public GameObject noSaveDataText, mainMenuUI, savedText;
    public Image treasureChest0, treasureChest1, treasureChest2;
    public TextMeshProUGUI hpValue, atkValue, defValue;

    private void Awake()
    {
        instance = this;
    }
    public void StartNewGame()
    {
        gameState.StartNewGame();
        mainMenuUI.SetActive(false);
    }
    public void ContinueGame()
    {
        if (gameState.LoadSaveGame())
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
        switch (index)
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
    public void UpdateCharHud(GameObject character)
    {

        SpritCharScript charScript;
        if (character.TryGetComponent<SpritCharScript>(out charScript))
        {
            hpValue.text = $"{charScript.HP}/{charScript.MaxHP}";
            atkValue.text = charScript.ATK.ToString();
            defValue.text = charScript.DEF.ToString();
        }
    }
    public void MainMenu()
    {
        TwoDGameState.Instance.ReturnToMainMenu();
        mainMenuUI.SetActive(true);
    }
    public void SaveGame()
    {
        try
        {
            TwoDGameState.Instance.SaveData();
            StartCoroutine(ResetSavedText());
        }
        catch
        {
            Debug.LogWarning("Failed To Save");
        }
    }
    IEnumerator ResetSavedText()
    {
        savedText.SetActive(true);
        yield return new WaitForSeconds(3);
        savedText.SetActive(false);
    }
}
