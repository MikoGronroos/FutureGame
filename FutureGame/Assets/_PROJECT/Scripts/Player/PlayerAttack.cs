using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{

    [SerializeField] private float allowedDistance;
    [SerializeField] private float damage;

    private CharacterOwner _charOwner;
    private Camera _camera;

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

    public void Attack()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray,  out var hit))
        {
            var distance = Vector3.Distance(_charOwner.transform.position, hit.transform.position);

            if (distance > allowedDistance) return;

            var hittedObj = hit.transform.GetComponent<IDamageable>();
            if (hittedObj != null)
            {
                hittedObj.MakeDamage(damage);
            }
        }
    }
}
