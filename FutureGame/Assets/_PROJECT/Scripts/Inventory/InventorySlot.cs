﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private bool isEmpty;
    [SerializeField] private int itemID;
    [SerializeField] private int stackSize;
    [SerializeField] private List<Item> items = new List<Item>();

    private Image _image;
    private Sprite _icon;
    private TextMeshProUGUI _stackSizeText;

    public bool IsEmpty { get { return isEmpty; } private set { } }
    public int StackSize { get { return stackSize; } set { stackSize = value; } }
    public int ItemID { get { return itemID; } set { itemID = value; } }

    private void OnValidate()
    {
        _stackSizeText = GetComponentInChildren<TextMeshProUGUI>();
        RefreshStackSizeText();
        CheckEmptyState();
    }

    private void Awake()
    {
        _stackSizeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshStackSizeText();
        CheckEmptyState();
    }

    private void RefreshStackSizeText()
    {
        _stackSizeText.text = GetItemAmount().ToString();
    }

    private void CheckEmptyState()
    {
        if (items.Count <= 0)
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
            RefreshSlotImage();
        }
        items.Add(item);
        RefreshStackSizeText();
    }

    public int GetItemAmount()
    {
        return items.Count;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            Debug.Log($"You clicked on {items[0]}");
            Inventory.Instance.Equip(items[0]);
            items.Remove(items[0]);
            CheckEmptyState();
            RefreshSlotImage();
            RefreshStackSizeText();
        }
    }
}
