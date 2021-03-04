using UnityEngine;

public class HitDetection : MonoBehaviour
{

    private Collider _damageCollider;

    public float Damage;

    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    public void DisableCollider()
    {
        _damageCollider.enabled = false;
    }

    public void EnableCollider()
    {
        _damageCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.MakeDamage(Damage);
        }
    }
}