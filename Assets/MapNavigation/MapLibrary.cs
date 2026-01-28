using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MapLibrary", menuName = "Scriptable Objects/MapLibrary")]
public class MapLibrary : ScriptableObject
{
    public List<GameMap> mapLibrary;
}
