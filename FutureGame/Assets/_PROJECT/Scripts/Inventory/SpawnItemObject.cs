﻿using UnityEngine;

public class SpawnItemObject : MonoBehaviour
{

    private static SpawnItemObject _instance;

    public static SpawnItemObject Instance
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

    public void SpawnItem(Vector3 pos, GameObject itemObj)
    {
        GameObject item = Instantiate(itemObj, pos, Quaternion.identity);
    }

}