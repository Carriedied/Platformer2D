using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(HealthCharacter))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyAttackZone))]
[RequireComponent(typeof(VisiblePlayer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WallsDetected _wallsDetector;

    private Patrol _patrol;
    private HealthCharacter _healthCharacter;
    private EnemyMover _enemyMover;
    private EnemyAnimator _enemyAnimator;
    private EnemyAttackZone _enemyAttackZone;
    private VisiblePlayer _visiblePlayer;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _healthCharacter = GetComponent<HealthCharacter>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _visiblePlayer = GetComponent<VisiblePlayer>();
    }

    private void Update()
    {
        if (_patrol.Movement != Vector2.zero)
        {
            _enemyAnimator.RunAnimation(true);

            _enemyMover.Move(_patrol.Movement);
        }
        else
        {
            _enemyAnimator.RunAnimation(false);
        }

        if (_patrol.GetIsVisiblePlayer() && _patrol.GetIsPossibleAttack())
        {
            Penguin player = _patrol.GetPlayer();

            if (player != null && _enemyAttackZone.GetIsAttakZone(player))
            {
                _enemyAnimator.StartAttackAnimation();
                _enemyMover.Attack(player);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _healthCharacter.TakeAwayHealth(damage);
    }
}
