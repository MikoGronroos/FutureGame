using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform equipmentParent;
    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    private CharacterOwner _charOwner;

    private Dictionary<int, ItemAmountInInventory> _itemsInInventory = new Dictionary<int, ItemAmountInInventory>();

    private void Awake()
    {
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
            }else if (slots[i].AmountOfItems < slots[i].StackSize)
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

        if (!_itemsInInventory.ContainsKey(id))
        {
            ItemAmountInInventory item = new ItemAmountInInventory(id);
            _itemsInInventory.Add(id, item);
            Debug.Log($"{item.ToString()}");
        }
        else
        {
            _itemsInInventory[id].AddItem();
        }

        freeSlot.AddItemToSlot(ItemDictionary.Instance.GetItemByID(id));
        CharacterOwner.Instance.CharacterStats.CurrentWeight += ItemDictionary.Instance.GetItemByID(id).Weight;

    }

    private void AddToEquipmentSlot(int slotIndex, Item item)
    {
        if (equipmentSlots[slotIndex].GetCurrentItem() != null)
        {
            Dequip(item);
        }
        equipmentSlots[slotIndex].SetCurrentItem(item);
        if (item is Equipment)
        {
            var equipment = item as Equipment;
            GameObject itemObject = Instantiate(equipment.EquipmentObject, handTransform.position, Quaternion.Euler(handTransform.eulerAngles.x + equipment.EquipmentObject.transform.eulerAngles.x
                , handTransform.eulerAngles.y + equipment.EquipmentObject.transform.eulerAngles.y
                , handTransform.eulerAngles.z + equipment.EquipmentObject.transform.eulerAngles.z)
                , handTransform);
            itemObject.name = equipment.ItemName;
            equipmentSlots[slotIndex].SetItemGameObject(itemObject);
            if (equipment is Weapon)
            {
                var weapon = equipment as Weapon;
                _charOwner.PlayerAttack.ChangeStats(weapon.damage, weapon.range, weapon.speed);
            }
        }
    }

    public void Equip(Item item)
    {
        Debug.Log("Using item");
        AddToEquipmentSlot(FindEquipmentSlotWithTag(item), item);
    }

    public void Dequip(Item item)
    {

        var slot = equipmentSlots[FindEquipmentSlotWithTag(item)];

        if (slot.GetCurrentObject())
        {
            Destroy(slot.GetCurrentObject());
        }
        AddItem(slot.GetCurrentItem().ItemID);
    }

    public int FindEquipmentSlotWithTag(Item item)
    {
        var equipment = item as Equipment;
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipment.EquipmentType == equipmentSlots[i].GetEquipmentType())
            {
                return i;
            }
        }
        return 0;
    }

    private InventorySlot GetSlot(int id)
    {
        int i = 0;
        for (; i < slots.Length; i++)
        {
            if (slots[i].AmountOfItems < slots[i].StackSize && slots[i].ItemID == id)
            {
                return slots[i];
            }
            else if (slots[i].IsEmpty)
            {
                return slots[i];
            }
        }
        return null;
    }

    public InventorySlot GetSlotWithItem(int id)
    {
        int i = 0;
        for (; i < slots.Length; i++)
        {
            if (slots[i].ItemID == id)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void RemoveItemFromInventoryWithId(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemID == id)
            {
                slots[i].RemoveItem();
                _itemsInInventory[id].RemoveItem();
                CharacterOwner.Instance.CharacterStats.CurrentWeight -= ItemDictionary.Instance.GetItemByID(id).Weight;
            }
        }
    }

    public void RemoveItemFromSlot(InventorySlot slot)
    {
        slot.RemoveItem();
        CharacterOwner.Instance.CharacterStats.CurrentWeight -= ItemDictionary.Instance.GetItemByID(slot.ItemID).Weight;
    }

    #region InventoryDictionary

    public void RemoveItemFromDictionaryWithKey(int key)
    {
        _itemsInInventory[key].RemoveItem();
    }

    public int GetAmountOfItemsInDictionary(int key)
    {
        if (!_itemsInInventory.ContainsKey(key)) return 0;

        return _itemsInInventory[key].GetAmountOfItems();
    }

    public void RemoveItemCompletelyFromDictionary(int key)
    {
        _itemsInInventory.Remove(key);
    }

    public ItemAmountInInventory GetItemFromDictionary(int key)
    {
        return _itemsInInventory[key];
    }

    public void ReduceOrRemoveItemFromDictionary(int itemID)
    {
        if (_charOwner.Inventory.GetAmountOfItemsInDictionary(itemID) > 1)
        {
            _charOwner.Inventory.RemoveItemFromDictionaryWithKey(itemID);
            Debug.Log($"{_charOwner.Inventory.GetItemFromDictionary(itemID).ToString()} has been reduced by 1");
        }
        else
        {
            Debug.Log($"{_charOwner.Inventory.GetItemFromDictionary(itemID).ToString()} has been removed completely!");
            _charOwner.Inventory.RemoveItemCompletelyFromDictionary(itemID);
        }
    }

    #endregion

}

public class ItemAmountInInventory
{
    int _itemId;
    int _amountOfItems = 0;

    public ItemAmountInInventory(int id)
    {
        _itemId = id;
        AddItem();
    }

    public void AddItem()
    {
        _amountOfItems++;
    }

    public void RemoveItem()
    {
        _amountOfItems--;
    }

    public int GetAmountOfItems()
    {
        return _amountOfItems;
    }

    public override string ToString()
    {
        return $"item id = {_itemId}, item amount = {_amountOfItems}";
    }

}

