using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{

    [SerializeField] private EquipmentType slotType;
    [SerializeField] private Item currentItem;

    private Sprite _slotDefaultSprite;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _slotDefaultSprite = _image.sprite;
    }

}
