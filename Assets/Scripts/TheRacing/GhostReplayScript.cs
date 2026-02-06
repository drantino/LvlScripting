using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
public class GhostReplayScript : MonoBehaviour
{
    public string ghostDataFilePath;
    public GameState gameState;
    public GhostData replayData;
    public int vehicleType, ghostIndex;
    public Color vehicleColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        //load ghost data Assets/Resources/GhostData.csv
        if(File.Exists(ghostDataFilePath))
        {
            string[] lines = File.ReadAllLines(ghostDataFilePath);
            string[] firstColumn = Regex.Split(lines[0], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            vehicleType = int.Parse(firstColumn[6]);
            vehicleColor = new Color(int.Parse(firstColumn[7]) / 255f, int.Parse(firstColumn[8]) / 255f, int.Parse(firstColumn[9]) / 255f);
            for(int index = 1; index < lines.Length;index++)
            {
                string[] column = Regex.Split(lines[index], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                replayData.AddFrame(new Vector3(float.Parse(column[0]), float.Parse(column[1]), float.Parse(column[2])), new Vector3(float.Parse(column[3]), float.Parse(column[4]), float.Parse(column[5])));
            }
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameState.currentGameState == GameState.RacingGameState.Racing && ghostIndex < replayData.ghostDataFrames.Count)
        {
            transform.position = replayData.ghostDataFrames[ghostIndex].Position;
            transform.rotation = Quaternion.Euler(replayData.ghostDataFrames[ghostIndex].Rotation.x, replayData.ghostDataFrames[ghostIndex].Rotation.y, replayData.ghostDataFrames[ghostIndex].Rotation.z);
            ghostIndex++;
        }

    }
    public void UpdateGhostVehicleLooks()
    {

    }
}
