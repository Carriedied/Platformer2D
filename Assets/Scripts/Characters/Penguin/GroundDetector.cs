using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundDetector : MonoBehaviour
{
    private int _groundCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Tilemap>(out _))
        {
            _groundCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Tilemap>(out _))
        {
            _groundCount--;
        }  
    }

    public bool IsGround()
    {
        return _groundCount > 0;
    }
}
