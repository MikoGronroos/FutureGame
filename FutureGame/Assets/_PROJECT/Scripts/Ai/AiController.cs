using UnityEngine;

public class AiController : MonoBehaviour
{

    [SerializeField] private int confidenceLevel;
    [SerializeField] private bool canRoamAround;

    [SerializeField] private float maxRoamRange = 70f;
    [SerializeField] private float minRoamRange = 10f;

    [SerializeField] private GameObject player;

    private AiPathfinding _aiPathfinding;
    private AiFieldOfView _aiFieldOfView;

    private Vector3 _startingPosition;
    private Vector3 _roamPosition;

    private void Awake()
    {
        _aiFieldOfView = GetComponent<AiFieldOfView>();
        _aiPathfinding = GetComponent<AiPathfinding>();
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        _startingPosition = transform.position;
        _roamPosition = _aiPathfinding.RandomNavSphere(_startingPosition, Random.Range(minRoamRange, maxRoamRange), -1);
    }

    private void Update()
    {
        if (canRoamAround)
        {
            _aiPathfinding.SetAgentDestination(_roamPosition);

            float reachedPositionDistance = 1f;
            if (Vector3.Distance(transform.position, _roamPosition) < reachedPositionDistance)
            {
                _roamPosition = _aiPathfinding.RandomNavSphere(_startingPosition, Random.Range(minRoamRange, maxRoamRange), -1);
            }
        }
    }

    public void HasSeenPlayer()
    {
        Debug.Log($"{transform.name} saw player?");
    }

}
