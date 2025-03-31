using UnityEngine;
using UnityEngine.Tilemaps;

public class WallsDetected : MonoBehaviour
{
    private int _wallCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Tilemap>(out _))
        {
            _wallCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Tilemap>(out _))
        {
            _wallCount--;
        }
    }

    public bool GetIsWall()
    {
        return _wallCount > 0;
    }
}
