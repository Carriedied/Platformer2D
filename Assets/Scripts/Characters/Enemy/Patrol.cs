using UnityEngine;

[RequireComponent(typeof(CharacterDetected))]
[RequireComponent(typeof(VisiblePlayer))]
[RequireComponent(typeof(WallsDetected))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _attackCooldown;

    private CharacterDetected _playerDetected;
    private VisiblePlayer _visiblePlayer;
    private WallsDetected _wallsDetected;

    private Penguin? _player;
    private int _currentWaypoint = 0;
    private float _threshold = 0.2f;
    private float _lastAttackTime = 0f;
    private float _timePass;
    private bool _isDetectedPlayer;
    private bool _isVisiblePlayer;
    private bool _isPossibleAttack;
    private float _zeroMovement = 0;

    public Vector2 Movement { get; private set; }
    public bool IsDetectedPlayer { get; private set; }

    private void Awake()
    {
        _playerDetected = GetComponent<CharacterDetected>();
        _visiblePlayer = GetComponent<VisiblePlayer>();
        _wallsDetected = GetComponent<WallsDetected>();
    }

    private void Start()
    {
        DefinitionPressure(_waypoints[_currentWaypoint].position.x);

        _playerDetected.OnPlayerDetected += DetectedPlayer;
    }

    private void Update()
    {
        if ((transform.position - _waypoints[_currentWaypoint].transform.position).sqrMagnitude < _threshold * _threshold && _isDetectedPlayer == false)
        {
            Bypass();
        }

        if (_isDetectedPlayer == true && _player != null)
        {
            _visiblePlayer.CheckWalls(_player);

            if (_visiblePlayer.GetIsGround())
            {
                DefinitionPressure(_waypoints[_currentWaypoint].position.x);

                _isDetectedPlayer = false;
            }
            else
            {
                _isVisiblePlayer = true;

                DefinitionPressure(_player.transform.position.x);
            }

            if (_isDetectedPlayer == true && _wallsDetected.GetIsWall())
            {
                DefinitionPressure(_zeroMovement);
            }

            if (_isDetectedPlayer == false && _wallsDetected.GetIsWall())
            {
                DefinitionPressure(0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isDetectedPlayer && _player != null)
        {
            _timePass = _lastAttackTime + _attackCooldown;

            if (Time.time >= _timePass)
            {
                _isPossibleAttack = true;

                _lastAttackTime = Time.time;
            }
        }
    }

    public bool GetIsVisiblePlayer() => GetBoolAsTrigger(ref _isVisiblePlayer);

    public bool GetIsPossibleAttack() => GetBoolAsTrigger(ref _isPossibleAttack);

    public Penguin GetPlayer()
    {
        return _player;
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }

    private void DefinitionPressure(float PositionXWaypoint)
    {
        if (transform.position.x < PositionXWaypoint)
        {
            Movement = Vector2.right;
        }
        else
        {
            Movement = Vector2.left;
        }

        if (PositionXWaypoint == _zeroMovement)
        {
            Movement = Vector2.zero;
        }
    }

    private void Bypass()
    {
        _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        DefinitionPressure(_waypoints[_currentWaypoint].position.x);
    }

    private void DetectedPlayer(Penguin player)
    {
        _playerDetected.OnPlayerDetected -= DetectedPlayer;
        _playerDetected.PlayerUndetected += UndetectedPlayer;

        _isDetectedPlayer = true;

        _player = player;
    }

    private void UndetectedPlayer()
    {
        _playerDetected.OnPlayerDetected += DetectedPlayer;
        _playerDetected.PlayerUndetected -= UndetectedPlayer;

        _isDetectedPlayer = false;

        _player = null;

        DefinitionPressure(_waypoints[_currentWaypoint].position.x);
    }
}