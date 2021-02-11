﻿using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{

    [SerializeField] private Item[] items;

    private Dictionary<int, Item> _allItems = new Dictionary<int, Item>();

    private static ItemDictionary _instance;

    public static ItemDictionary Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        foreach (Item item in items)
        {
            if (_allItems.ContainsKey(item.Id))
            {
                Debug.Log("Dictionary Contains This Key Already");
                continue;
            }
            _allItems.Add(item.Id, item);
        }
        if (items.Length == _allItems.Count)
        {
            Debug.Log("All Items Were Loaded");
        }
        else
        {
            Debug.LogWarning("Not All Items Were Loaded");
        }

    }

    public Item GetItemByID(int id)
    {
        if (_allItems.ContainsKey(id))
        {
            return _allItems[id];
        }
        else
        {
            Debug.LogWarning($"The key {id} is not available in current item dictionary!");
            return null;
        }
    }

}
