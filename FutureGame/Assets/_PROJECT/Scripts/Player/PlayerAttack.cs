using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] private bool hasAttacked;

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

    #region Stats Changers

    public void ChangeStats(float damage, float dist, float speed)
    {
        SetDamage(damage);
        SetSpeed(speed);
    }

    private void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void SetDamage(float damage)
    {
        this.damage = damage;
    }

    #endregion

    IEnumerator WhileAttacking()
    {

        hasAttacked = true;

        yield return new WaitForSeconds(speed);

        animator.SetBool("isAttacking", false);
        hasAttacked = false;

        yield return null;
    }

    public void Attack()
    {

        if (hasAttacked)
        {
            return;
        }
        StartCoroutine(WhileAttacking());

        animator.SetBool("isAttacking", true);
    }
}
