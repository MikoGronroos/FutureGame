using UnityEngine;

public class Bed : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("Spawnpoint set.");
        WorldManager.Instance.SpawnPoint.SpawnPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }

}
