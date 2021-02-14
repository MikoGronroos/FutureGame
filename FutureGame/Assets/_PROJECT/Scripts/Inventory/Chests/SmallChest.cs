﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmallChest : MonoBehaviour, IInteractable
{

    [SerializeField] private bool isOpen;
    [SerializeField] private Transform chestSlotParent;
    [SerializeField] private Button closeChestButton;

    [SerializeField] private ItemContainer itemContainer;
    [SerializeField] private ContainerData containerData;

    private GameObject smallChestUI;

    private void Awake()
    {

        smallChestUI = FindObjectOfType<SmallChestUI>().gameObject;

        Debug.Log(smallChestUI.name);

        itemContainer.InitContainer(smallChestUI);
        containerData = new ContainerData(itemContainer.GetContainerSize());
        closeChestButton.onClick.AddListener(CloseChest);
    }

    private void Start()
    {
        containerData.AddItemToContents(ItemDictionary.Instance.GetItemByID(1), 3);
        containerData.AddItemToContents(ItemDictionary.Instance.GetItemByID(1), 4);
        containerData.AddItemToContents(ItemDictionary.Instance.GetItemByID(1), 5);
        containerData.AddItemToContents(ItemDictionary.Instance.GetItemByID(5), 6);
        containerData.AddItemToContents(ItemDictionary.Instance.GetItemByID(7), 7);
    }

    public void Interact()
    {
        StartCoroutine(RefreshSlots(Callback));
    }

    private IEnumerator RefreshSlots(Action callback)
    {

        for (int i = 0; i < containerData.GetContents().Length; i++)
        {

            if (containerData.GetContents()[i] == null) continue;

            chestSlotParent.GetChild(i).GetComponent<InventorySlot>().RefreshItem(containerData.GetContents()[i].ThisItem);
            chestSlotParent.GetChild(i).GetComponent<InventorySlot>().CurrentAmountOfItems++;
        }

        yield return null;

        callback();
    }

    private void Callback()
    {
        isOpen = itemContainer.ToggleContainer(isOpen);
    }

    private void CloseChest()
    {
        isOpen = itemContainer.ToggleContainer(isOpen);
    }

}

public class ContainerData
{

    private StoredItem[] _contents;

    public ContainerData(int amountOfSlots)
    {
        _contents = new StoredItem[amountOfSlots];
    }

    public void AddItemToContents(Item item, int index)
    {

        if (_contents.Length <= 0)
        {
            Debug.LogWarning($"Contents size is {_contents.Length}. Cant add items!");
            return;
        }

        if (_contents[index] != null)
        {
            _contents[index].CurrentAmount++;
            return;
        }

        _contents[index] = new StoredItem(item);
    }

    public StoredItem[] GetContents()
    {
        return _contents;
    }

}