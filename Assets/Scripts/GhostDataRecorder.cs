using UnityEngine;

public class GhostDataRecorder : MonoBehaviour
{
    public GhostData ghostData = new GhostData();
    public bool recording;
    public GameState gameState;
    
    private void Start()
    {
        if (gameState == null)
        {
            gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        }
    }
    private void FixedUpdate()
    {
        if (!recording && gameState.currentGameState != GameState.RacingGameState.Racing)
        {
            return;
        }
        ghostData.AddFrame(transform.position, transform.rotation.eulerAngles);
    }
    public void StartRecording()
    {
        recording = true;
    }
    public void SaveGhostData()
    {

    }
}
