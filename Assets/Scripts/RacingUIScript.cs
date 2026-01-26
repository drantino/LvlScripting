using TMPro;
using UnityEngine;

public class RacingUIScript : MonoBehaviour
{
    public GameState gameState;
    public float timeLaps;
    public TMP_Text timeTxtValue;
    public GameObject pausePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameState.currentGameState == GameState.RacingGameState.Racing)
        {
            timeLaps += Time.fixedDeltaTime;
            timeTxtValue.text = (timeLaps / 60 - (timeLaps / 60 % 1)).ToString() + ":" + (timeLaps % 60 - (timeLaps % 60 % 0.01)).ToString();
        }
    }
}
