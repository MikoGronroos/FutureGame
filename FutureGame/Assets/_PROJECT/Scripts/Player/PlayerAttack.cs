using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{

    [SerializeField] private float damage;

    [SerializeField] private bool hasAttacked;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform rightHandTransform;

    [SerializeField] private HitDetection _currentHitDetection;
    private CharacterOwner _charOwner;

    public float Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        _charOwner = GetComponentInParent<CharacterOwner>();
    }

    private void Start()
    {
        LoadWeapon(10, 0.7f, _currentHitDetection);
    }

    private void Update()
    {
        if (_charOwner.Input.HitInput())
        {
            Attack();
        }
    }

    public void LoadWeapon(float damage, float speed, HitDetection detector)
    {
        SetDamage(damage);
        SetCurrentHitDetection(detector);
        LoadStatsToCurrentHitDetector();
    }

    #region Stats Changers

    private void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void SetCurrentHitDetection(HitDetection detector)
    {
        _currentHitDetection = detector;
    }

    private void LoadStatsToCurrentHitDetector()
    {

        if (_currentHitDetection == null)
        {
            return;
        }

        _currentHitDetection.Damage = damage;
    }

    #endregion

    public void Attack()
    {
        if (hasAttacked)
        {
            return;
        }
        animator.SetBool("isAttacking", true);
        hasAttacked = true;
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        hasAttacked = false;
    }

    #region Handle Damage Collider

    public void EnableRightHandDamageCollider()
    {
        _currentHitDetection.EnableCollider();
    }

    public void DisableRightHandDamageCollider()
    {
        _currentHitDetection.DisableCollider();
    }

    #endregion

}
