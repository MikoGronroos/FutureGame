using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private bool isEmpty;
    [SerializeField] private int itemID;
    [SerializeField] private int stackSize;
    [SerializeField] private int amountOfItems;

    [SerializeField] private Item item;

    private Image _image;
    private Sprite _icon;
    private TextMeshProUGUI _stackSizeText;
    private CharacterOwner _charOwner;
    private ItemInfoPanelManager _itemInfoPanelManager;

    public bool IsEmpty { get { return isEmpty; } private set { } }
    public int StackSize { get { return stackSize; } set { stackSize = value; } }
    public int ItemID { get { return itemID; } set { itemID = value; } }
    public int AmountOfItems { get { return amountOfItems; } private set { } }

    private void OnValidate()
    {
        _stackSizeText = GetComponentInChildren<TextMeshProUGUI>();
        RefreshStackSizeText();
        CheckEmptyState();
    }

    private void Awake()
    {
        _charOwner = FindObjectOfType<CharacterOwner>();
        _stackSizeText = GetComponentInChildren<TextMeshProUGUI>();
        _itemInfoPanelManager = FindObjectOfType<ItemInfoPanelManager>();
    }

    private void Start()
    {
        RefreshStackSizeText();
        CheckEmptyState();
    }

    private void RefreshStackSizeText()
    {
        _stackSizeText.text = amountOfItems.ToString();
    }

    private void CheckEmptyState()
    {
        if (amountOfItems <= 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }

    public void RefreshSlotImage()
    {
        if (isEmpty)
        {
            _image.sprite = null;
        }
        else
        {
            _image.sprite = _icon;
        }
    }

    public void AddItemToSlot(Item item)
    {
        if (isEmpty)
        {
            if (!_image) _image = GetComponent<Image>();

            _icon = item.Icon;
            stackSize = item.StackSize;
            itemID = item.ItemID;
            isEmpty = false;
            this.item = item;
            amountOfItems++;
            RefreshSlotImage();
        }
        else
        {
            amountOfItems++;
        }
        RefreshStackSizeText();
    }

    public void RemoveItem()
    {
        if (amountOfItems > 1)
        {
            amountOfItems--;
        }
        else
        {
            item = null;
            amountOfItems--;
        }
        CheckEmptyState();
        RefreshSlotImage();
        RefreshStackSizeText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Debug.Log(position);
            _itemInfoPanelManager.PressedInventorySlot(this, position, true);
            return;
        }

        if (!isEmpty)
        {
            Debug.Log($"You clicked on {item}");
            item.Use(this);
            CheckEmptyState();
            RefreshSlotImage();
            RefreshStackSizeText();
        }
    }
}
