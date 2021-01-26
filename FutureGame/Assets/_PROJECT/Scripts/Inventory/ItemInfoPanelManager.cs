using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelManager : MonoBehaviour
{

    [SerializeField] private bool inventoryIsActive;

    [SerializeField] private GameObject InfoPanel;

    [SerializeField] private Button discardAllButton;
    [SerializeField] private Button discardSingleButton;

    private Inventory _inventory;
    private InventorySlot _currentInventorySlot;
    private int _amountOfItems;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        discardSingleButton.onClick.AddListener(DiscardSingleItem);
        discardAllButton.onClick.AddListener(DiscardAllItems);
    }

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("InventoryToggle", ReceiveMessage);
    }

    private void Update()
    {
        if (!inventoryIsActive) return;

        /*

        if (Input.GetMouseButtonDown(0))
        {
            SetObjectActive(false);
        }
        */
    }

    public void PressedInventorySlot(InventorySlot slot, Vector3 position, bool value)
    {
        _currentInventorySlot = slot;
        RefreshAmountOfItems(_currentInventorySlot.AmountOfItems);
        SetPosition(position);
        SetObjectActive(value);

    }

    private void ReceiveMessage(string name, string message)
    {
        if (inventoryIsActive)
        {
            inventoryIsActive = false;
        }
        else
        {
            inventoryIsActive = true;
        }
    }

    private void SetObjectActive(bool value)
    {
        InfoPanel.SetActive(value);
    }

    private void RefreshAmountOfItems(int value)
    {
        _amountOfItems = value;
    }

    private void SetPosition(Vector3 position)
    {
        InfoPanel.transform.localPosition = position;
    }

    private void DiscardSingleItem()
    {
        _inventory.RemoveItemFromSlot(_currentInventorySlot);
        RefreshAmountOfItems(_currentInventorySlot.AmountOfItems);
    }

    private void DiscardAllItems()
    {
        for (int i = 0; i < _amountOfItems; i++)
        {
            _inventory.RemoveItemFromSlot(_currentInventorySlot);
        }
        RefreshAmountOfItems(_currentInventorySlot.AmountOfItems);
    }

}
