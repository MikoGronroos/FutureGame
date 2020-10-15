using UnityEngine;

public class ItemOnGround : MonoBehaviour
{

    [SerializeField] private int itemID;

    public int GetID()
    {
        return itemID;
    }

    public void SetID(int id)
    {
        itemID = id;
    }

}
