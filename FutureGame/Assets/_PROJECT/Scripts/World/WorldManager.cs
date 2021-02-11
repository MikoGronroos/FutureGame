using UnityEngine;

public class WorldManager : MonoBehaviour
{

    private WorldSpawnPoint _spawnPoint;
    private DayNightCycle _dayNightCycle;
    private WorldClock _worldClock;

    public WorldSpawnPoint SpawnPoint { get { return _spawnPoint; } }
    public DayNightCycle DayNightCycle { get { return _dayNightCycle; } }
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
        _dayNightCycle = GetComponent<DayNightCycle>();
        _worldClock = GetComponent<WorldClock>();
    }

}
