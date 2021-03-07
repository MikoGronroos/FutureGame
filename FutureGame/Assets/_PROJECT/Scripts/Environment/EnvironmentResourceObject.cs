using UnityEngine;

public class EnvironmentResourceObject : MonoBehaviour
    , IDamageable
{

    [SerializeField] private LootTable thisLoot;
    [SerializeField] private float health;

    public void MakeDamage(float amount)
    {
        health -= amount;
        if (CheckHealth(health))
        {
            CollectObject();
        }
    }

    private void CollectObject()
    {
        DecideLoot();
        Destroy(gameObject);
    }

    private void DecideLoot()
    {
        if (thisLoot != null)
        {
            Item current = thisLoot.LootItem();
            if (current != null)
            {
                Inventory.Instance.AddToInventory(current, 1);
            }
        }
    }

    private bool CheckHealth(float health)
    {
        return health <= 0;
    }
}
