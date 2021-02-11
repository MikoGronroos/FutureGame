using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Finark/Loot/LootTable")]
public class LootTable : ScriptableObject
{

    public LootItem[] Loot;

}

[Serializable]
public class LootItem
{
    [Tooltip("Item that gets dropped")]
    public Item Item;

    [Tooltip("0%-100% probability of dropping")]
    [Range(0,100)]
    public int Probability;

    [Tooltip("Amount Of Drops That Player Gets")]
    public int AmountOfDrops;

}
