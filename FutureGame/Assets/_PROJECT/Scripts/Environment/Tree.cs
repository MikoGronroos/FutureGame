using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{

    [SerializeField] private float health;
    [SerializeField] private GameObject TreeItem;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        SpawnItemObject.Instance.SpawnItem(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), TreeItem);
        Destroy(gameObject);
    }

    private bool CheckHealth(float health)
    {
        return health <= 0;
    }

}
