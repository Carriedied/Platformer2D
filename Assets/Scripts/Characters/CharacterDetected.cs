using System;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterDetected : MonoBehaviour
{
    [SerializeField] private float _radiusDetected;

    public event Action<Penguin> OnPlayerDetected;
    public event Action<Enemy> OnEnemyDetected;
    public event Action PlayerUndetected;
    public event Action EnemyUndetected;

    private Collider2D[] _characters;
    private bool _isPlayerDetected;
    private bool _isEnemyDetected;

    private void Update()
    {
        _characters = Physics2D.OverlapCircleAll(transform.position, _radiusDetected);

        _isPlayerDetected = false;
        _isEnemyDetected = false;

        foreach (var hero in _characters)
        {
            if (hero.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _isEnemyDetected = true;

                OnEnemyDetected?.Invoke(enemy);
            }

            if (hero.TryGetComponent<Penguin>(out Penguin player))
            {
                _isPlayerDetected = true;

                OnPlayerDetected?.Invoke(player);
            }
        }

        if (_isEnemyDetected == false)
        {
            EnemyUndetected?.Invoke();
        }

        if (_isPlayerDetected == false)
        {
            PlayerUndetected?.Invoke();
        }
    }
}
