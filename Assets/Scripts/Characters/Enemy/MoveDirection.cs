using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    public void RedirectEnemy(float PositionXWaypoint)
    {
        if (transform.position.x < PositionXWaypoint)
        {
            Movement = Vector2.right;
        }
        else if (transform.position.x > PositionXWaypoint)
        {
            Movement = Vector2.left;
        }
        else
        {
            Movement = Vector2.zero;
        }
    }
}
