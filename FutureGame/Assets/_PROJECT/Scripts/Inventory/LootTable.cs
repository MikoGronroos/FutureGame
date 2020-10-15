using System;
using UnityEngine;

[Serializable]
public class LootTable
{

    [SerializeField] private int[] table;
    [SerializeField] private int[] ids;

    [SerializeField] private int total;
    [SerializeField] private int randomNumber;
    [SerializeField] private int amountOfLoot;

    public int[] GetLoot()
    {

        int[] loot = new int[amountOfLoot];

        foreach (var item in table)
        {
            total += item;
        }

        for (int i = 0; i < amountOfLoot; i++)
        {

            randomNumber = UnityEngine.Random.Range(0, total);

            foreach (var weight in table)
            {
                if (randomNumber <= weight)
                {
                    for (int j = 0; j < table.Length; j++)
                    {
                        if (weight == table[j])
                        {
                            loot[i] = ids[j];
                            break;
                        }
                    }
                }
                else
                {
                    randomNumber -= weight;
                }
            }
        }
        return loot;
    }

}
