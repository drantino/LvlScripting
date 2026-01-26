using System.IO;
using UnityEngine;

public class GhostDataRecorder : MonoBehaviour
{
    public GhostData ghostData = new GhostData();
    public bool recording;
    public GameState gameState;
    public string ghostDataFilePath;

    private void Start()
    {
        if (gameState == null)
        {
            gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        }
    }
    private void FixedUpdate()
    {
        if (!recording || gameState.currentGameState != GameState.RacingGameState.Racing)
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
        //Assets/Resources/GhostData.csv
        using (StreamWriter writer = new StreamWriter(ghostDataFilePath, false))
        {
            writer.WriteLine($"PositionX,PositionY,PositionZ,RotationX,RotationY,RotationZ,{gameState.currentProfile.vehicleType},{gameState.currentProfile.vehicleColorR},{gameState.currentProfile.vehicleColorG},{gameState.currentProfile.vehicleColorB}");
            for(int index = 0; index < ghostData.ghostDataFrames.Count; index++)
            {
                writer.WriteLine($"{ghostData.ghostDataFrames[index].Position.x.ToString("F2")},{ghostData.ghostDataFrames[index].Position.y.ToString("F2")},{ghostData.ghostDataFrames[index].Position.z.ToString("F2")},{ghostData.ghostDataFrames[index].Rotation.x.ToString("F2")},{ghostData.ghostDataFrames[index].Rotation.y.ToString("F2")},{ghostData.ghostDataFrames[index].Rotation.z.ToString("F2")}");
            }
        }
    }
}
