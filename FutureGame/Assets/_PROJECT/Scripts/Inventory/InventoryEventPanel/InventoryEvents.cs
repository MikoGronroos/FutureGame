using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{

    [SerializeField] private Color AddColor;
    [SerializeField] private Color RemoveColor;

    private static InventoryEvents _instance;

    public static InventoryEvents Instance
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

    public void AddItem()
    {

    }

    public void RemoveItem()
    {

    }

}
