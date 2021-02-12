using UnityEngine;

public class WorldManager : MonoBehaviour
{

    private WorldSpawnPoint _spawnPoint;
    private DayCycle _dayCycle;
    private WorldClock _worldClock;

    public WorldSpawnPoint SpawnPoint { get { return _spawnPoint; } }
    public DayCycle DayCycle { get { return _dayCycle; } }
    public WorldClock WorldClock { get { return _worldClock; } }

    #region Singleton

    private static WorldManager _instance;

    public static WorldManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
        _spawnPoint = GetComponent<WorldSpawnPoint>();
        _dayCycle = GetComponent<DayCycle>();
        _worldClock = GetComponent<WorldClock>();
    }

}
