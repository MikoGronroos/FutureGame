using UnityEngine;

public class SpawnItemObject : MonoBehaviour
{

    [SerializeField] private GameObject GlobalItemObject;

    private static SpawnItemObject _instance;

    public static SpawnItemObject Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SpawnItem(Vector3 pos, int id)
    {
        GameObject item = Instantiate(GlobalItemObject, pos, Quaternion.identity);
        item.GetComponent<ItemOnGround>().SetID(id);
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
