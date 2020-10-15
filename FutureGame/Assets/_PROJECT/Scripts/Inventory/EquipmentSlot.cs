using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{

    [SerializeField] private ItemType slotType;
    [SerializeField] private Item currentItem;

    private Sprite _slotDefaultSprite;
    private Image _image;
    private GameObject itemObject;

    private void Start()
    {
        _image = GetComponent<Image>();
        _slotDefaultSprite = _image.sprite;
    }

    public ItemType GetEquipmentType()
    {
        return slotType;
    }

    public void SetCurrentItem(Item item)
    {
        currentItem = item;
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

}
