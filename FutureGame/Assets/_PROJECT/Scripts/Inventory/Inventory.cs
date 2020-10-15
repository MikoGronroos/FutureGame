using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private Transform handTransform;
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

    private void Dequip(EquipmentSlot slot)
    {
        if (slot.GetEquipmentType() != ItemType.Consumable)
        {
            Destroy(slot.GetCurrentObject());
        }
        AddItem(slot.GetCurrentItem().ItemID);
    }

    private void AddToEquipmentSlot(int slotIndex, Item item)
    {
        if (equipmentSlots[slotIndex].GetCurrentItem() != null)
        {
            Dequip(equipmentSlots[slotIndex]);
        }
        equipmentSlots[slotIndex].SetCurrentItem(item);
        if (item is Equipment)
        {
            var equipment = item as Equipment;
            GameObject itemObject = Instantiate(equipment.EquipmentObject, handTransform.position, handTransform.rotation, handTransform);
            itemObject.name = equipment.ItemName;
            equipmentSlots[slotIndex].SetItemGameObject(itemObject);
        }
    }

    public void Equip(Item item)
    {
        Debug.Log("Using item");
        if (item is Resource)
        {
            Debug.Log("Item is Resource");
            return;
        }
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (item.Type == equipmentSlots[i].GetEquipmentType())
            {
                AddToEquipmentSlot(i, item);
            }
        }
    }

    private InventorySlot GetSlot(int id)
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
