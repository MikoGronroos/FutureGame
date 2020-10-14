using UnityEngine;

public class FallDamage : MonoBehaviour
{

    [SerializeField] private float damageThreshold;

    private IDamageable _damageable;
    private GroundCheck _groundCheck;

    private bool _startedFalling;

    private int startYPos = 0;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        FallDamageCalculation();
    }

    private void FallDamageCalculation()
    {
        if (!_startedFalling && !_groundCheck.Grounded())
        {
            startYPos = Mathf.RoundToInt(transform.position.y);
            _startedFalling = true;
        }
        if (_groundCheck.Grounded() && _startedFalling)
        {
            int endYPos = Mathf.RoundToInt(transform.position.y);
            int value = startYPos - endYPos;
            if (startYPos - endYPos <= damageThreshold)
            {
                _startedFalling = false;
                return;
            }
            float damage = (startYPos - endYPos - damageThreshold) * (float)1.61;
            _damageable.MakeDamage(damage);
            Debug.Log($"Fell and took {damage} amount of damage");
            _startedFalling = false;
            return;
        }
    }
}
