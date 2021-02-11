using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ItemContainer
{

    [SerializeField] private int containerSize;

    [SerializeField] private GameObject slotGameObject;

    public int GetContainerSize()
    {
        return containerSize;
    }

    public GameObject GetSlotGameObject()
    {
        return slotGameObject;
    }

}
