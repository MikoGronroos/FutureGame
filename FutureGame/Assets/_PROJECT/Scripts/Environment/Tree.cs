using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{

    [SerializeField] private float health;
    [SerializeField] private GameObject TreeItem;
    [SerializeField] private LootTable lootTable;

    public void MakeDamage(float amount)
    {
        health -= amount;
        if (CheckHealth(health))
        {
            TreeFall();
        }
    }

    private void TreeFall()
    {
        Debug.Log("Tree has fallen");
        var loot = lootTable.GetLoot();
        for (int i = 0; i < loot.Length; i++)
        {
            SpawnItemObject.Instance.SpawnItem(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), TreeItem, loot[i]);
        }
        Destroy(gameObject);
    }

    private bool CheckHealth(float health)
    {
        return health <= 0;
    }

}
