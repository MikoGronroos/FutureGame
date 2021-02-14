using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour, IEndDragHandler, IDragHandler
{

    [SerializeField] private Item thisItem;

    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private Vector3 _dragStartPos;
    private Slot _lastSlot;

    public bool DroppedOnSlot;

    public Item ThisItem
    {
        get
        {
            return thisItem;
        }
        set
        {
            thisItem = value;
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            RefreshImage(thisItem);
        }
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
        _image = GetComponent<Image>();
    }

    private void RefreshImage(Item item)
    {
        _image.sprite = item.Icon;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.57f;
        _canvasGroup.blocksRaycasts = false;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!DroppedOnSlot)
        {
            transform.position = _dragStartPos;
            _lastSlot.RefreshItem(thisItem);
            _lastSlot.DeleteInventoryObject(gameObject);
            _lastSlot.CurrentAmountOfItems++;
            return;
        }
        DroppedOnSlot = false;
    }

    public void SetDragStartPos(Vector3 pos)
    {
        _dragStartPos = pos;
    }

    public void SetLastSlot(Slot slot)
    {
        _lastSlot = slot;
    }

    public Slot GetLastSlot()
    {
        return _lastSlot;
    }


}
