using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public void Spawn(Vector3 point)
    {
        transform.position = point;
    }

}
