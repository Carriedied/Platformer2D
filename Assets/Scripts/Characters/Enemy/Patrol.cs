using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

[RequireComponent(typeof(VisiblePlayer))]
[RequireComponent(typeof(WallsDetected))]
[RequireComponent(typeof(CharacterDetector))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _attackCooldown;

    private VisiblePlayer _visiblePlayer;
    private WallsDetected _wallsDetected;
    private CharacterDetector _enemyDetected;

    private Penguin _player;
    private int _currentWaypoint = 0;
    private float _threshold = 0.2f;
    private float _lastAttackTime = 0f;
    private float _timePass;
    private bool _isDetectedPlayer;
    private bool _isPossibleAttack;
    private float _zeroMovement = 0;

    public Vector2 Movement { get; private set; }
    public bool IsDetectedPlayer { get; private set; }

    private void Awake()
    {
        _visiblePlayer = GetComponent<VisiblePlayer>();
        _wallsDetected = GetComponent<WallsDetected>();
        _enemyDetected = GetComponent<CharacterDetector>();
    }

    private void Start()
    {
        DefinitionPressure(_waypoints[_currentWaypoint].position.x);
    }

    private void Update()
    {
        if ((transform.position - _waypoints[_currentWaypoint].transform.position).sqrMagnitude < _threshold * _threshold && _isDetectedPlayer == false)
        {
            Bypass();
        }

        if (_enemyDetected.TryGetTarget(out ITarget target))
        {
            if (target is Penguin penguin)
            {
                _player = penguin;

                if (_visiblePlayer.CheckWalls(_player))
                {
                    DefinitionPressure(_waypoints[_currentWaypoint].position.x);
                    _isDetectedPlayer = false;
                }
                else
                {
                    _isDetectedPlayer = true;
                    DefinitionPressure(_player.transform.position.x);
                }
            }
            else
            {
                _isDetectedPlayer = false;
                _player = null;
            }
        }
        else
        {
            _isDetectedPlayer = false;
            _player = null;
        }

        if (_isDetectedPlayer == true && _wallsDetected.IsWall())
        {
            DefinitionPressure(_zeroMovement);
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

    public bool GetIsVisiblePlayer() => GetBoolAsTrigger(ref _isDetectedPlayer);

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
}