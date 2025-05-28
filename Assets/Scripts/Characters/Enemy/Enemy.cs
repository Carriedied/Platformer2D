using Assets.Scripts.Characters.Penguin.Interfaces;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAttackZone))]
[RequireComponent(typeof(AttackerBase))]
[RequireComponent(typeof(MoveDirection))]
[RequireComponent(typeof(Pursuit))]
public class Enemy : MonoBehaviour, ITarget
{
    [SerializeField] private WallsDetected _wallsDetector;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private EnemyMover _enemyMover;
    private EnemyAttackZone _enemyAttackZone;
    private AttackerBase _enemyAttacker;
    private MoveDirection _moveDirection;
    private Pursuit _pursuit;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _enemyAttacker = GetComponent<AttackerBase>();
        _moveDirection = GetComponent<MoveDirection>();
        _pursuit = GetComponent<Pursuit>();
    }

    private void Update()
    {
        if (_moveDirection.Movement != Vector2.zero)
        {
            _enemyAnimator.RunAnimation(true);

            _enemyMover.Move(_moveDirection.Movement);
        }
        else
        {
            _enemyAnimator.RunAnimation(false);
        }

        if (_enemyAttacker.GetIsAttack())
        {
            Penguin player = _pursuit.GetPlayer();

            if (player != null)
            {
                Health healthPlayer = player.GetComponent<Health>();

                if (healthPlayer != null && _enemyAttackZone.GetIsAttakZone(player))
                {
                    _enemyAnimator.StartAttackAnimation();

                    _enemyAttacker.Attack(healthPlayer);
                }
            }
        }
    }

    public Health GetHealthComponent()
    {
        return GetComponent<Health>();
    }
}
