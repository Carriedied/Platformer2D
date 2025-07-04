using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private float _speedX;
    [SerializeField] private float _jumpForceY;

    private Rigidbody2D _physicalProperty;

    private float _jumpForceX = 0;
    private float _offsetY = 0;
    private float _offsetZ = 0;

    private void Awake()
    {
        _physicalProperty = GetComponent<Rigidbody2D>();
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

    public void AttackMove(Enemy opponent)
    {
        float directionAttack;

        directionAttack = opponent.transform.position.x - transform.position.x;

        _flipper.TurnAround(directionAttack);
    }
}
