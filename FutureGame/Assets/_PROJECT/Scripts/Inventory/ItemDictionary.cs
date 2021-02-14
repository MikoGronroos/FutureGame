using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{

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

        Item[] items = Resources.LoadAll<Item>("Items/");

        foreach (Item item in items)
        {
            if (_allItems.ContainsKey(item.Id))
            {
                Debug.Log($"Dictionary Contains Key {item.Id} Already");
                continue;
            }
            _allItems.Add(item.Id, item);
        }

        Debug.Log($"Item Dictionary contains {_allItems.Count} unique items!");

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
