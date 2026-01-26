using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class RaceControllerScript : MonoBehaviour
{
    public GameState gameState;
    public VehicleController playerVehicleControllerScript;
    public List<GhostData> loadedGhostData = new List<GhostData>();
    public string ghostDataFilePath;
    public CheckPointControllerScript checkPointControllerScript;
    public RacingUIScript raceUIScript;
    public float timeTillStart;
    InputAction startButton;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        if (loadedGhostData == null && File.Exists(ghostDataFilePath))
        {
            //load ghost data
        }
        startButton = InputSystem.actions.FindAction("Pause");
        startButton.Enable();
        
    }
    private void Update()
    {

        if (startButton.WasReleasedThisFrame())
        {
            if (gameState.currentGameState == GameState.RacingGameState.Racing)
            {
                gameState.currentGameState = GameState.RacingGameState.GamePaused;
                raceUIScript.OpenPauseMenu();
                Time.timeScale = 0;
            }
            else if (gameState.currentGameState == GameState.RacingGameState.GamePaused)
            {
                gameState.currentGameState = GameState.RacingGameState.Racing;
                raceUIScript.ClosePauseMenu();
                Time.timeScale = 1;
            }
        }
    }
    private void FixedUpdate()
    {
        switch (gameState.currentGameState)
        {
            case GameState.RacingGameState.MainMenu:
                {
                    gameState.currentGameState = GameState.RacingGameState.PreRace;

                    break;
                }
            case GameState.RacingGameState.PreRace:
                {
                    timeTillStart -= Time.deltaTime;

                    raceUIScript.StartTimeCountDown(timeTillStart);
                    if (timeTillStart <= 0)
                    {
                        gameState.currentGameState = GameState.RacingGameState.Racing;
                    }
                    break;
                }
            case GameState.RacingGameState.Racing:
                {

                    break;
                }
            case GameState.RacingGameState.PostRace:
                {

                    break;
                }
            case GameState.RacingGameState.GamePaused:
                {

                    break;
                }
        }

    }
    public void RaceFinish()
    {
        gameState.currentGameState = GameState.RacingGameState.PostRace;
        raceUIScript.FinishRaceUIUpdate();
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode Mode)
    {
        timeTillStart = 3;
    }

}
