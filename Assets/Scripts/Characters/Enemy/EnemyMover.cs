using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flipper))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speedX;

    private Rigidbody2D _physicalProperty;
    private Flipper _flipper;

    private Vector2 _movement;

    private void Awake()
    {
        _physicalProperty = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void Move(Vector2 direction)
    {
        _flipper.TurnAround(direction.x);

        _movement = direction.normalized * _speedX * Time.fixedDeltaTime;

        Vector2 newPosition = _physicalProperty.position + _movement;

        _physicalProperty.MovePosition(newPosition);
    }
}
