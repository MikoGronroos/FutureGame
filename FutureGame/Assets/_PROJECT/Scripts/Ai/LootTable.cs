using UnityEngine;

[System.Serializable]
public class Loot
{
    public Item ThisLoot;
    public float LootChance;
}

[CreateAssetMenu(menuName = "Finark/LootTable")]
public class LootTable : ScriptableObject
{

    public Loot[] Loots;

    public Item LootItem()
    {

        float cumProb = 0;
        float currentProb = Random.Range(0, 100);

        for (int i = 0; i < Loots.Length; i++)
        {
            cumProb += Loots[i].LootChance;
            if(currentProb <= cumProb)
            {
                Debug.Log($"{Loots[i].ThisLoot} has been given with probability of {currentProb}!");
                return Loots[i].ThisLoot;
            }
        }

        return null;
    }

}
