using Assets.Scripts.Characters.Penguin.Interfaces;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyAttackZone))]
[RequireComponent(typeof(VisiblePlayer))]
[RequireComponent(typeof(AttackerBase))]
public class Enemy : MonoBehaviour, ITarget
{
    [SerializeField] private WallsDetected _wallsDetector;

    private Patrol _patrol;
    private EnemyMover _enemyMover;
    private EnemyAnimator _enemyAnimator;
    private EnemyAttackZone _enemyAttackZone;
    private AttackerBase _enemyAttacker;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAttackZone = GetComponent<EnemyAttackZone>();
        _enemyAttacker = GetComponent<AttackerBase>();
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

            Health healthPlayer = player.GetComponent<Health>();

            if (player != null && _enemyAttackZone.GetIsAttakZone(player))
            {
                _enemyAnimator.StartAttackAnimation();

                _enemyAttacker.Attack(healthPlayer);
            }
        }
    }
}
