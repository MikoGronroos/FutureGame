using UnityEngine;

public class WorldSpawnPoint : MonoBehaviour
{

    private Vector3 _spawnPoint = new Vector3(2, 10, 5);

    public Vector3 SpawnPoint { get { return _spawnPoint; } set { _spawnPoint = value; } }

}
