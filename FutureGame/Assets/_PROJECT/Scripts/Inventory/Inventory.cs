using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject _characterInfoPanel;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    [SerializeField] private ItemContainer container;

    private bool _inventoryIsOpen;

    [SerializeField] private List<StoredItem> _inventoryItems = new List<StoredItem>();

    #region Singleton

    private static Inventory _instance;

    public static Inventory Instance { get { return _instance; } }

    #endregion

    #region Awake, Start and Update

    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }

        container.InitContainer(_characterInfoPanel);

    }

    private void Start()
    {
        for (int i = 0; i < container.GetContainerSize(); i++)
        {
            GameObject slot = Instantiate(container.GetSlotGameObject(), Vector3.zero, Quaternion.identity, inventoryParent);
            slots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    private void Update()
    {
        if (CharacterOwner.Instance.Input.InventoryInput())
        {
            if (_inventoryIsOpen)
            {
                _inventoryIsOpen = container.ToggleContainer(_inventoryIsOpen);
                MessageSender.SendMessageToClients("InventoryToggle");
            }
            else
            {
                _inventoryIsOpen = container.ToggleContainer(_inventoryIsOpen);
                MessageSender.SendMessageToClients("InventoryToggle");
            }
        }
    }

    #endregion

    #region Inventory Slot Manipulation

    private InventorySlot GetValidSlotForItem(Item item)
    {
        if (item.IsStackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {

                if (slots[i].GetCurrentItem() is null)
                {
                    continue;
                }

                if (slots[i].GetCurrentItem().Id == item.Id && !slots[i].IsSlotFull)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }

    private InventorySlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsSlotEmpty)
            {
                return slots[i];
            }
        }
        return null;
    }

    public InventorySlot GetSlotWithItemId(int id)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetCurrentItem() == ItemDictionary.Instance.GetItemByID(id))
            {
                return slots[i];
            }
        }
        return null;
    }

    #endregion

    //Call this when picking up item from ground. Searches for first empty slot and instantiates item there.
    public StoredItem GetItemFromItemStorage(Item item)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].ThisItem == item)
            {
                return _inventoryItems[i];
            }
        }
        StoredItem emptyItem = new StoredItem(null);
        return emptyItem;
    }

    public bool CheckIfInventoryHasAmountOfItems(int id, int amount)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].ThisItem.Id == id && _inventoryItems[i].CurrentAmount >= amount)
            {
                return true;
            }
        }
        return false;
    }

    public void AddToInventory(Item item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            InventorySlot slot = GetValidSlotForItem(item);

            if (GetItemFromItemStorage(item).ThisItem != item)
            {
                StoredItem storedItem = new StoredItem(item);
                _inventoryItems.Add(storedItem);
            }
            else
            {
                GetItemFromItemStorage(item).CurrentAmount++;
            }

            if (slot == null)
            {
                slot = GetEmptySlot();
                if (slot == null)
                {
                    return;
                }
            }

            slot.AddItem(item);
        }
    }

    #region Inventory Items List

    public bool InventoryHasItem(int id)
    {
        Item itemToSearch = ItemDictionary.Instance.GetItemByID(id);
        foreach (StoredItem item in _inventoryItems)
        {
            if (item.GetThisItem() == itemToSearch)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemFromInventoryWithId(int id, int amount)
    {
        if (InventoryHasItem(id))
        {
            for (int i = 0; i < amount; i++)
            {
                InventorySlot slot = GetSlotWithItemId(id);
                GetItemFromItemStorage(ItemDictionary.Instance.GetItemByID(id)).CurrentAmount--;
                slot.RemoveItem();
            }
        }
    }

    #endregion

}

[System.Serializable]
public class StoredItem
{
    public Item ThisItem;

    //Amount of one item in inventory
    public int CurrentAmount;

    public StoredItem(Item item)
    {
        ThisItem = item;
        CurrentAmount = 1;
    }

    public Item GetThisItem()
    {
        return ThisItem;
    }

}

