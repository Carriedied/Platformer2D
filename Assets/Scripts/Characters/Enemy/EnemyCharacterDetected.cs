using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyCharacterDetected : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _radiusDetected;

    private Collider2D[] _detectedColliders = new Collider2D[5];
    private bool _isPlayerDetected;
    private int _minDetectedCollidersSize = 0;

    public event Action<Penguin> OnPlayerDetected;
    public event Action PlayerUndetected;

    private void Update()
    {
        _isPlayerDetected = false;

        Array.Clear(_detectedColliders, _minDetectedCollidersSize, _detectedColliders.Length);

        Physics2D.OverlapCircleNonAlloc(transform.position, _radiusDetected, _detectedColliders, _playerMask);

        foreach (var hero in _detectedColliders)
        {
            if (hero != null && hero.TryGetComponent<Penguin>(out Penguin player))
            {
                _isPlayerDetected = true;

                OnPlayerDetected?.Invoke(player);
            }
        }

        if (_isPlayerDetected == false)
        {
            PlayerUndetected?.Invoke();
        }
    }
}
