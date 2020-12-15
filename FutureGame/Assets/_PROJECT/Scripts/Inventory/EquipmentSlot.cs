using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private bool isEmpty;
    [SerializeField] private EquipmentType slotType;
    [SerializeField] private Item currentItem;

    private Sprite _icon;
    private Sprite _slotDefaultSprite;
    private Image _image;
    private GameObject itemObject;

    private void Start()
    {
        _image = GetComponent<Image>();
        _slotDefaultSprite = _image.sprite;
    }

    private void CheckEmptyState()
    {
        if (currentItem != null)
        {
            isEmpty = false;
        }
        else
        {
            isEmpty = true;
        }
    }

    private void RefreshSlotImage()
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


    public EquipmentType GetEquipmentType()
    {
        return slotType;
    }

    public void SetCurrentItem(Item item)
    {
        currentItem = item;
        _icon = item.Icon;
        CheckEmptyState();
        RefreshSlotImage();
    }

    public Item GetCurrentItem()
    {
        return currentItem;
    }

    public GameObject GetCurrentObject()
    {
        return itemObject;
    }

    public void SetItemGameObject(GameObject obj)
    {
        itemObject = obj;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentItem.Use(null);
    }
}
