using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform equipmentParent;
    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    private CharacterOwner _charOwner;

    private static Inventory _instance;

    public static Inventory Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _charOwner = GetComponent<CharacterOwner>();
    }


    private void OnValidate()
    {
        if (inventoryParent != null)
        {
            slots = inventoryParent.GetComponentsInChildren<InventorySlot>();
        }
        if (equipmentParent != null)
        {
            equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(id);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < 10; i++)
            {
                AddItem(id);
            }
        }
    }

    private bool IsFull()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                return false;
            }else if (slots[i].GetItemAmount() < slots[i].StackSize)
            {
                return false;
            }
        }
        return true;
    }

    public void AddItem(int id)
    {
        if (IsFull()) return;

        var freeSlot = GetSlot(id);
        freeSlot.AddItemToSlot(ItemDictionary.Instance.GetItemByID(id));
        Debug.Log($"Added {ItemDictionary.Instance.GetItemByID(id)} to inventory!");
    }

    public void UseItem(Item item)
    {
        item.Use(_charOwner.CharacterStats);
    }

    public InventorySlot GetSlot(int id)
    {
        int i = 0;
        for (; i < slots.Length; i++)
        {
            if (slots[i].GetItemAmount() < slots[i].StackSize && slots[i].ItemID == id)
            {
                Debug.Log($"This was sent from StackSize {i}");
                return slots[i];
            }
            else if (slots[i].IsEmpty)
            {
                Debug.Log($"This was sent from IsEmpty {i}");
                return slots[i];
            }
        }
        return null;
    }
}
