using System;
using UnityEngine;

public class PlayerCharacterDetected : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private float _radiusDetected;

    private Collider2D[] _detectedColliders = new Collider2D[5];
    private bool _isPlayerDetected;
    private int _minDetectedCollidersSize = 0;

    public event Action<Enemy> OnEnemyDetected;
    public event Action EnemyUndetected;

    private void Update()
    {
        _isPlayerDetected = false;

        Array.Clear(_detectedColliders, _minDetectedCollidersSize, _detectedColliders.Length);

        Physics2D.OverlapCircleNonAlloc(transform.position, _radiusDetected, _detectedColliders, _enemyMask);

        foreach (var hero in _detectedColliders)
        {
            if (hero != null && hero.TryGetComponent<Enemy>(out Enemy opponent))
            {
                _isPlayerDetected = true;

                OnEnemyDetected?.Invoke(opponent);
            }
        }

        if (_isPlayerDetected == false)
        {
            EnemyUndetected?.Invoke();
        }
    }
}
