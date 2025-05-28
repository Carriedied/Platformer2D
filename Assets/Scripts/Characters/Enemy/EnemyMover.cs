using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private Flipper _flipper;

    private Rigidbody2D _physicalProperty;

    private void Awake()
    {
        _physicalProperty = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _speedX;

        _flipper.TurnAround(direction.x);

        _physicalProperty.linearVelocity = new Vector2(movement.x, _physicalProperty.linearVelocity.y);
    }
}
