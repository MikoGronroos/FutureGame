using UnityEngine;

public class CharacterOwner : MonoBehaviour
{

    private int _playerId;

    private IInput _input;
    private IMovement _movement;
    private IAnimator _animator;
    private IGroundCheck _groundCheck;
    private PlayerUI _playerUI;
    private CharacterStats _characterStats;
    private SpawnPlayer _spawnPlayer;

    public IInput Input { get { return _input; } private set { } }
    public IMovement Movement { get { return _movement; } private set { } }
    public IAnimator Animator { get { return _animator; } private set { } }
    public IGroundCheck GroundCheck { get { return _groundCheck; } private set { } }
    public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
    public CharacterStats CharacterStats { get { return _characterStats; } private set { } }
    public SpawnPlayer SpawnPlayer { get { return _spawnPlayer; } private set { } }

    private static CharacterOwner _instance;

    public static CharacterOwner Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _input = GetComponent<IInput>();
        _movement = GetComponent<IMovement>();
        _animator = GetComponent<IAnimator>();
        _groundCheck = GetComponent<IGroundCheck>();
        _playerUI = GetComponent<PlayerUI>();
        _characterStats = GetComponent<CharacterStats>();
        _spawnPlayer = GetComponent<SpawnPlayer>();
    }

    private void Start()
    {
        GlobalPlayerManager.Instance.AddPlayerToList(this);
    }

}
