using UnityEngine;
using UnityEngine.Tilemaps;

public class VisiblePlayer : MonoBehaviour
{
    private bool _isGround;

    public void CheckWalls(Penguin player)
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized, distance);

        Debug.DrawRay(transform.position, direction * distance, Color.red);

        foreach (var hit in hits)
        {
            if (hit.collider is TilemapCollider2D)
            {
                _isGround = true;
            }
        }
    }

    public bool GetIsGround() => GetBoolAsTrigger(ref _isGround);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
