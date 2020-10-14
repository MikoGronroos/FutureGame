using UnityEngine;

public class PickUpItems : MonoBehaviour
{


    [SerializeField] private bool canPickup;

    private CharacterOwner _charOwner;
    private int _itemID;
    private GameObject _itemOnGround;

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
    }

    private void Update()
    {
        if (_charOwner.Input.InteractInput() && canPickup)
        {
            _charOwner.Inventory.AddItem(_itemID);
            _itemID = 0;
            canPickup = false;
            Destroy(_itemOnGround);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemOnGround>())
        {
            _itemID = other.GetComponent<ItemOnGround>().GetID();
            _itemOnGround = other.gameObject;
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ItemOnGround>())
        {
            _itemID = 0;
            canPickup = false;
        }
    }

}
