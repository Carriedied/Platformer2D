using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(AttackZone))]
[RequireComponent(typeof(AttackerBase))]
public class Penguin : MonoBehaviour, ITarget
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PenguinAnimator _playerAnimator;

    private Mover _mover;
    private InputReader _inputReader;
    private AttackZone _attackZone;
    private AttackerBase _attackerBase;

    public bool IsDetectedEnemy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _attackZone = GetComponent<AttackZone>();
        _attackerBase = GetComponent<AttackerBase>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _mover.Move(_inputReader.Direction);
            _playerAnimator.RunAnimation(true);
        }
        else
        {
            _playerAnimator.RunAnimation(false);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround())
            _mover.Jump();

        if (_inputReader.GetIsAttack() && IsDetectedEnemy && _attackerBase.GetIsAttack())
        {
            Enemy opponent = _attackZone.NinjaFrog;

            if (opponent != null)
            {
                Health opponentHealth = opponent.GetHealthComponent();

                if (opponentHealth != null)
                {
                    _playerAnimator.StartAttackAnimation();

                    _mover.AttackMove(opponent);

                    _attackerBase.Attack(opponentHealth);
                }
            }
        }
    }

    public void ChangeDetectedEnemy(bool isDetected)
    {
        IsDetectedEnemy = isDetected;
    }

    public Health GetHealthComponent()
    {
        return GetComponent<Health>();
    }
}
