using UnityEngine;

[RequireComponent(typeof(MoveDirection))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypoint = 0;
    private float _threshold = 0.2f;

    private MoveDirection _moveDirection;

    private void Awake()
    {
        _moveDirection = GetComponent<MoveDirection>();
    }

    private void Start()
    {
        _moveDirection.RedirectEnemy(_waypoints[_currentWaypoint].position.x);
    }

    private void Update()
    {
        if ((transform.position - _waypoints[_currentWaypoint].transform.position).sqrMagnitude < _threshold * _threshold)
        {
            Bypass();
        }
    }

    private void Bypass()
    {
        _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        _moveDirection.RedirectEnemy(_waypoints[_currentWaypoint].position.x);
    }
}