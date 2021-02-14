using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject _characterInfoPanel;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private List<InventorySlot> slots;

    [SerializeField] private ItemContainer container;

    private bool _inventoryIsOpen;

    private List<StoredItem> _inventoryItems = new List<StoredItem>();

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
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItemToInventory(ItemDictionary.Instance.GetItemByID(1));
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

    #endregion

    //Call this when picking up item from ground. Searches for first empty slot and instantiates item there.
    public StoredItem InventoryStorageHasItem(Item item)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].ThisItem == item)
            {
                return _inventoryItems[i];
            }
        }
        return null;
    }

    public void AddItemToInventory(Item item)
    {
        bool itemIsForEmptySlot = false;
        InventorySlot slot = GetValidSlotForItem(item);

        if (InventoryStorageHasItem(item) == null)
        {
            StoredItem storedItem = new StoredItem(item);
            _inventoryItems.Add(storedItem);
        }
        else
        {
            InventoryStorageHasItem(item).CurrentAmount++;
        }

        if (slot == null)
        {
            slot = GetEmptySlot();
            itemIsForEmptySlot = true;
            if (slot == null)
            {
                return;
            }
        }

        if (itemIsForEmptySlot)
        {
            slot.RefreshItem(item);
            slot.CurrentAmountOfItems++;
        }
        else
        {
            slot.CurrentAmountOfItems++;
        }

    }

    #region Inventory Items List

    public StoredItem GetStoredItem(Item item)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].ThisItem.Equals(item))
            {
                return _inventoryItems[i];
            }
        }
        return null;
    }

    #endregion

}

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

}

