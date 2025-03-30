using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flipper))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _impactForce;

    private Rigidbody2D _physicalProperty;
    private Flipper _flipper;

    private float _jumpForceX = 0;
    private float _offsetY = 0;
    private float _offsetZ = 0;

    private void Awake()
    {
        _physicalProperty = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void Move(float direction)
    {
        transform.position += new Vector3(direction, _offsetY, _offsetZ) * _speedX * Time.deltaTime;

        _flipper.TurnAround(direction);
    }

    public void Jump()
    {
        _physicalProperty.AddForce(new Vector2(_jumpForceX, _jumpForceY), ForceMode2D.Impulse);
    }

    public void Attack(Enemy opponent)
    {
        float directionAttack;

        opponent.TakeDamage(_impactForce);

        directionAttack = opponent.transform.position.x - transform.position.x;

        _flipper.TurnAround(directionAttack);
    }
}
