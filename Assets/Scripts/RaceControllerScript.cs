using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class RaceControllerScript : MonoBehaviour
{
    public GameState gameState;
    public VehicleController playerVehicleControllerScript;
    public List<GhostData> loadedGhostData = new List<GhostData>();
    public string ghostDataFilePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        if(loadedGhostData == null && File.Exists(ghostDataFilePath))
        {
            //load ghost data
        }
    }

    
}
