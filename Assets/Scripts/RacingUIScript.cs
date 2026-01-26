using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RacingUIScript : MonoBehaviour
{
    public GameState gameState;
    public float timeLaps;
    public TMP_Text timeTxtValue, countDownTxt;
    public GameObject pausePanel, finishPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameState.currentGameState == GameState.RacingGameState.Racing)
        {
            timeLaps += Time.fixedDeltaTime;
            timeTxtValue.text = (timeLaps / 60 - (timeLaps / 60 % 1)).ToString() + ":" + (timeLaps % 60 - (timeLaps % 60 % 0.01)).ToString();
        }
    }
    public void FinishRaceUIUpdate()
    {
        finishPanel.SetActive(true);
    }
    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
    }
    public void StartTimeCountDown(float count)
    {
        switch (count)
        {
            case > 3:
                {
                    countDownTxt.text = 3.ToString();
                    break;
                }
            case > 2:
                {
                    countDownTxt.text = 2.ToString();
                    break;
                }
            case > 1:
                {
                    countDownTxt.text = 1.ToString();
                    break;
                }
            case > 0:
                {
                    countDownTxt.text = "GO";
                    StartCoroutine(TurnOffCountDownTxt());
                    break;
                }
        }
    }
    IEnumerator TurnOffCountDownTxt()
    {
        yield return new WaitForSeconds(1f);
        countDownTxt.gameObject.SetActive(false);
    }
}
