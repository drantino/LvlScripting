using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMap", menuName = "Scriptable Objects/GameMap")]
public class GameMap : ScriptableObject
{
    public GameObject preFab;
    public string mapName;
    public int mapID;
    public List<MapEntryPoint> entryPoints;
}
[Serializable]
public class MapEntryPoint
{
    public int engryPointID;
    public Vector3Int cell;
}