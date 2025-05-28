using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VisiblePlayer : MonoBehaviour
{
    public bool IsGround { get; private set; }

    public bool IsWallBetweenPlayerAndTarget(Penguin player)
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized, distance);

        Debug.DrawRay(transform.position, direction * distance, Color.red);

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<Penguin>(out _))
            {
                return false;
            }

            if (hit.collider is CompositeCollider2D)
            {
                return true;
            }
        }

        return true;
    }
}
