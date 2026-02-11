using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapNavigation : MonoBehaviour
{
    public static MapNavigation Instance;
    [SerializeField] private Transform player;
    [SerializeField] private MapLibrary library;
    [SerializeField] private Transform mapParent;
    private Dictionary<int,MapData> mapDictionary = new Dictionary<int,MapData>();
    [SerializeField] private GameObject currentMap;
    public UnityEvent OnMapEnter;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        InitializeMapDictionary();
    }
    private void InitializeMapDictionary()
    {
        mapDictionary.Clear();
        foreach(GameMap m in library.mapLibrary)
        {
            mapDictionary.Add(m.mapID, new MapData(m));
        }
    }
    public void GoToMap(int mapID, int portalID)
    {

        TwoDGameState.Instance.SaveGameState();
        currentMap.GetComponentInChildren<EnemySpawner>().ClearEnemies();

        Destroy(currentMap);

        currentMap = Instantiate(mapDictionary[mapID].preFab, mapParent);
        Grid g = mapParent.GetComponent<Grid>();
        player.position = g.GetCellCenterWorld(mapDictionary[mapID].entryPoints[portalID].cell);
        StartCoroutine(InitializeMap(mapID));
        OnMapEnter?.Invoke();

        //get the cell that we want the player to spawn in MapDictionary[mapID].enteryPoints[portalID].cell
        //conver the cell into world space. returns a Vector 3 the new position
        //set the position to the update value

        // add aesthhetics: screenfade, sfx, animations
    }
    private IEnumerator InitializeMap(int mapID)
    {
        yield return new WaitForEndOfFrame();
        TwoDGameState.Instance.InitializeMap(mapID );
    }
}
public class MapData
{
    public GameObject preFab;
    public string mapName;
    public int mapID;

    public Dictionary<int, MapEntryPoint> entryPoints = new Dictionary<int, MapEntryPoint>();

    public MapData(GameMap config)
    {
        this.preFab = config.preFab;
        this.mapID = config.mapID;
        mapName = config.mapName;

        foreach(MapEntryPoint m in config.entryPoints)
        {
            entryPoints.Add(m.engryPointID, m);
        }
    }
}