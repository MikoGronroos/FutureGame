﻿using UnityEngine;

[CreateAssetMenu(menuName = "Finark/Item")]
public class Item : ScriptableObject
{

    public string ItemName;
    public int ItemID;
    public int StackSize;
    public Sprite Icon;

    public virtual void Use(CharacterStats player)
    {

    }

}
