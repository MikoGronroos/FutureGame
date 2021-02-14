using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] private bool hasAttacked;

    [SerializeField] private LayerMask ignoreMask;

    [SerializeField] private float damageReduceMultiplier = 0.135f;
    [SerializeField] private Animator animator;

    private CharacterOwner _charOwner;
    private Camera _camera;

    public float Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        _charOwner = GetComponent<CharacterOwner>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_charOwner.Input.HitInput())
        {
            Attack();
        }
    }

    public void ChangeStats(float damage, float dist, float speed)
    {
        SetDamage(damage);
        SetRange(dist);
        SetSpeed(speed);
    }

    private void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void SetRange(float dist)
    {
        range = dist;
    }

    private void SetDamage(float damage)
    {
        this.damage = damage;
    }

    IEnumerator AttackSpeedCalc()
    {

        hasAttacked = true;

        yield return new WaitForSeconds(speed);

        animator.SetBool("isPunching", false);
        hasAttacked = false;

        yield return null;
    }

    public void Attack()
    {

        if (hasAttacked)
        {
            return;
        }
        else
        {
            StartCoroutine(AttackSpeedCalc());
        }

        Debug.Log("Attacked");
        animator.SetBool("isPunching", true);

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray,  out var hit, range, ~ignoreMask))
        {

            if (hit.transform.TryGetComponent(out IDamageable hittedObject))
            {
                hittedObject.MakeDamage(damage);
            }
        }
    }
}
