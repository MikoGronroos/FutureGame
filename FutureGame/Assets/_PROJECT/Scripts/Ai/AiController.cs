using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiFieldOfView), typeof(AiPathfinding), typeof(Animator))]
[RequireComponent(typeof(AiAttack))]
public class AiController : Character, IDamageable
{

    [Header("Ai Vitals")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina;

    [Header("Ai Personality")]
    [Range(0,100)]
    [SerializeField] private int confidenceLevel;

    [Range(0, 100)]
    [SerializeField] private int familiarityWithPlayer;

    [SerializeField] private float comfortableRangeFromPlayer;

    [Range(0,10)]
    [SerializeField] private int hearingLevel;

    [Range(0,1)]
    [SerializeField] private float reactionTime = 0.33f;

    [Range(0, 6)]
    [SerializeField] private float touchZoneRadius;

    [SerializeField] private float maxRoamRange = 70f;
    [SerializeField] private float minRoamRange = 10f;

    [SerializeField] private bool isHostile;

    [SerializeField] private bool canRoamAround;

    [SerializeField] private bool stopIfCloseToPlayer;

    [Header("Ai Combat")]
    [SerializeField] private List<Race> hostileTowardsRaces = new List<Race>();

    [Header("Ai Movement Speeds")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;

    [Header("Miscellanious")]
    [SerializeField] private bool isCloseToAPlayer;
    [SerializeField] private float damping;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private LayerMask targetMask;

    [Header("Loot")]
    [SerializeField] private LootTable thisLoot;

    [Header("State Machine")]
    [SerializeField] private AiState currentState;

    private AiPathfinding _aiPathfinding;
    private AiFieldOfView _aiFieldOfView;
    private AiAttack _aiAttack;
    private Animator _animator;

    private Vector3 _startingPosition;
    private Vector3 _roamPosition;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            float health = value;
            if (health > maxHealth)
            {
                currentHealth = maxHealth;
                return;
            }
            else if (health <= 0)
            {
                currentHealth = 0;
                currentState = AiState.dead;
                return;
            }
            else
            {
                currentHealth = health;
            }
            if (currentHealth < maxHealth * 0.2 && confidenceLevel <= 40)
            {
                currentState = AiState.fleeing;
            }
            else if (currentHealth < maxHealth * 0.09 && confidenceLevel > 41)
            {
                currentState = AiState.fleeing;
            }
            else if (currentHealth < maxHealth * 0.05)
            {
                currentState = AiState.fleeing;
            }
        }
    }

    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }

    public Transform CurrentTarget
    {
        get
        {
            return currentTarget;
        }
        set
        {
            isCloseToAPlayer = CheckIfTargetIsPlayer(value);
            currentTarget = value;
        }
    }

    public WeaponType WeaponType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        _aiFieldOfView = GetComponent<AiFieldOfView>();
        _aiPathfinding = GetComponent<AiPathfinding>();
        _aiAttack = GetComponent<AiAttack>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
        _startingPosition = transform.position;
        _aiPathfinding.SetAgentSpeed(walkingSpeed);
        if (canRoamAround)
        {
            currentState = AiState.roaming;
            _roamPosition = _aiPathfinding.RandomNavSphere(_startingPosition, Random.Range(minRoamRange, maxRoamRange), -1);
        }
        StartCoroutine(Touching(reactionTime));
    }

    private void Update()
    {
        StateMachine();
    }

    private void StateMachine()
    {
        switch (currentState)
        {
            #region roamingState
            case AiState.roaming:

                if (isCloseToAPlayer && stopIfCloseToPlayer)
                {
                    currentState = AiState.stopped;
                    return;
                }

                _aiPathfinding.SetAgentDestination(_roamPosition);

                float reachedPositionDistance = 3f;
                if (Vector3.Distance(transform.position, _roamPosition) < reachedPositionDistance)
                {
                    _roamPosition = _aiPathfinding.RandomNavSphere(_startingPosition, Random.Range(minRoamRange, maxRoamRange), -1);
                }
                break;
            #endregion
            #region fleeingState
            case AiState.fleeing:

                if (currentTarget == null)
                {
                    return;
                }

                Vector3 runDirection = (transform.position - currentTarget.position);

                Vector3 newPos = transform.position + runDirection * 3;

                _aiPathfinding.SetAgentDestination(newPos);

                if (Vector3.Distance(transform.position, currentTarget.position) > comfortableRangeFromPlayer)
                {
                    _startingPosition = transform.position;
                    _roamPosition = _aiPathfinding.RandomNavSphere(_startingPosition, Random.Range(minRoamRange, maxRoamRange), -1);
                    currentState = AiState.roaming;
                }
                break;
            #endregion
            #region aggressiveAttackState
            case AiState.aggressiveAttacking:

                _aiPathfinding.SetAgentDestination(currentTarget.position);

                break;
            #endregion
            #region passiveAttackingState
            case AiState.passiveAttacking:



                break;
                #endregion
            #region deadState
            case AiState.dead:

                DecideLoot();
                Destroy(gameObject);

                break;
            #endregion
            #region stoppedState
            case AiState.stopped:

                _aiPathfinding.SetAgentDestination(transform.position);

                var lookPos = CurrentTarget.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

                _aiPathfinding.ToggleMovement(false);

                break;
                #endregion
        }
    }

    private bool CheckIfAiIsHostileTowardsRace(Race race)
    {
        if (hostileTowardsRaces.Contains(race))
        {
            return true;
        }
        return false;
    }

    private bool CheckIfTargetIsPlayer(Transform target)
    {

        if (target == null)
        {
            return false;
        }

        return target.TryGetComponent(out CharacterOwner _character);
    }

    public void SawCharacter(Race race, Transform target)
    {

        if (target == currentTarget || target == transform)
        {
            return;
        }

        bool isDisliked = CheckIfAiIsHostileTowardsRace(race);
        CurrentTarget = target;

        if (isHostile && isDisliked)
        {
            if (confidenceLevel < 27)
            {
                currentState = AiState.fleeing;
            }
            else if (confidenceLevel <= 50)
            {
                currentState = AiState.passiveAttacking;
            }
            else if (confidenceLevel >= 51)
            {
                currentState = AiState.aggressiveAttacking;
            }
        }
        else
        {
            if (confidenceLevel < 55)
            {
                currentState = AiState.fleeing;
            }
            else if (confidenceLevel < 100 && familiarityWithPlayer > 50)
            {
                currentState = AiState.followingTarget;
            }
        }
    }

    private IEnumerator Touching(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            CurrentTarget = null;

            Collider[] objectThatTouch = Physics.OverlapSphere(transform.position, touchZoneRadius, targetMask);
            for (int i = 0; i < objectThatTouch.Length; i++)
            {
                if (objectThatTouch[i].TryGetComponent(out Character character) && objectThatTouch[i].transform != transform)
                {
                    if (CheckIfAiIsHostileTowardsRace(character.GetCharacterRace()))
                    {
                        CurrentTarget = objectThatTouch[i].transform;
                        break;
                    }

                    if (CheckIfTargetIsPlayer(objectThatTouch[i].transform))
                    {
                        CurrentTarget = objectThatTouch[i].transform;
                        break;
                    }
                }
            }
        }
    }

    private void DecideLoot()
    {
        if (thisLoot != null)
        {
            Item current = thisLoot.LootItem();
            if (current != null)
            {
            }
        }
    }

    public float GetReactionTime()
    {
        return reactionTime;
    }

    #region Overrides And Inheritances

    public void MakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }

    public override Race GetCharacterRace()
    {
        return base.GetCharacterRace();
    }

    #endregion

    private void OnDrawGizmos()
    {
        //Draw touch zone
        Gizmos.DrawWireSphere(transform.position, touchZoneRadius);
    }

}
