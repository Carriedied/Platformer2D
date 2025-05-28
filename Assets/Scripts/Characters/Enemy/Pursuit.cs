using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
[RequireComponent(typeof(VisiblePlayer))]
[RequireComponent(typeof(MoveDirection))]
public class Pursuit : MonoBehaviour
{
    private VisiblePlayer _visiblePlayer;
    private CharacterDetector _enemyDetected;
    private MoveDirection _moveDirection;

    private Penguin _player;
    private float _previousWaypoint;
    private bool _isLastWaypointRecord = false;

    private void Awake()
    {
        _visiblePlayer = GetComponent<VisiblePlayer>();
        _enemyDetected = GetComponent<CharacterDetector>();
        _moveDirection = GetComponent<MoveDirection>();
    }

    private void Update()
    {
        if (_enemyDetected.TryGetTarget(out ITarget target))
        {
            if (target is Penguin penguin)
            {
                _player = penguin;

                if (_visiblePlayer.IsWallBetweenPlayerAndTarget(_player) == false)
                {
                    if (_isLastWaypointRecord == false)
                    {
                        _previousWaypoint = _moveDirection.Movement.x;
                        _isLastWaypointRecord = true;
                    }

                    _moveDirection.RedirectEnemy(_player.transform.position.x);
                }
                else
                {
                    _moveDirection.RedirectEnemy(_previousWaypoint);
                }
            }
            else
            {
                _player = null;
            }
        }
        else
        {
            _isLastWaypointRecord = false;
        }
    }

    public Penguin GetPlayer()
    {
        return _player;
    }
}
