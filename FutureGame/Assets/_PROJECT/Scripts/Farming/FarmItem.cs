using UnityEngine;

public class FarmItem : MonoBehaviour, IDamageable
{

    [SerializeField] private float currentHealth = 0;

    [Header("Moisture")]
    [SerializeField] private float moistureLevel = 0;
    [SerializeField] private float moistureLeaveRate = 0;
    [SerializeField] private bool needsMoisture = true;

    [SerializeField] private float progression = 0;

    private bool _readyToHarvest;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private float _currentBlendShapeWeight;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        MessageReceiver.SubscrideToMessage("OnFarmingTick", OnFarmingTickListener);

        if (_skinnedMeshRenderer != null)
        {
            _currentBlendShapeWeight = _skinnedMeshRenderer.GetBlendShapeWeight(0);
        }

    }

    private void OnDisable()
    {
        MessageReceiver.UnsubscribeToMessage("OnFarmingTick", OnFarmingTickListener);
    }

    private void OnFarmingTickListener(string name, string content)
    {

        if (moistureLevel <= 0 && needsMoisture) return;

        Grow();
        CheckProgressionState();
    }

    private void Grow()
    {
        if (_skinnedMeshRenderer != null)
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Clamp(_currentBlendShapeWeight -= 1, 0, 100));
        }

        progression = Mathf.Clamp(progression += 1, 0, 100);

        if (needsMoisture)
        {
            moistureLevel = Mathf.Clamp(moistureLevel - moistureLeaveRate * Time.deltaTime, 0, 100);
        }
    }

    private void CheckProgressionState()
    {
        if (progression >= 100)
        {
            _readyToHarvest = true;
        }
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            CheckProgression();
            Destroy(gameObject);
        }
    }

    private void CheckProgression()
    {
        if (progression >= 100)
        {
            //Drop Items
        }
    }

    public void MakeDamage(float amount)
    {
        currentHealth -= amount;
        CheckHealth();
    }

    public void AddMoisture(float amount)
    {
        if (moistureLevel < 100)
        {
            moistureLevel += amount;
        }
    }

    public bool GetNeedMoisture()
    {
        return needsMoisture;
    }

}
