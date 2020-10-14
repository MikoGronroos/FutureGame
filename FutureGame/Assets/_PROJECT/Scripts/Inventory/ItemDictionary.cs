using System.Collections.Generic;
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
    }

    private void Start()
    {
        foreach (Item item in items)
        {
            if (_allItems.ContainsKey(item.ItemID))
            {
                Debug.Log("Dictionary Contains This Key Already");
                continue;
            }
            _allItems.Add(item.ItemID, item);
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
        Debug.Log(id);
        return _allItems[id];
    }

}
