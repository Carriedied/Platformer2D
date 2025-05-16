using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PenguinAnimator))]
[RequireComponent(typeof(AttackZone))]
[RequireComponent(typeof(AttackerBase))]
public class Penguin : MonoBehaviour, ITarget
{
    [SerializeField] private GroundDetector _groundDetector;

    private Mover _mover;
    private InputReader _inputReader;
    private PenguinAnimator _playerAnimator;
    private AttackZone _attackZone;
    private AttackerBase _attackerBase;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PenguinAnimator>();
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

        if (_inputReader.GetIsAttack() && _attackZone.GetIsPossibleAttack())
        {
            Enemy opponent = _attackZone.NinjaFrog;
            Health opponentHealth = opponent.GetComponent<Health>();

            if (opponent != null)
            {
                _playerAnimator.StartAttackAnimation();

                _mover.AttackMove(opponent);

                _attackerBase.Attack(opponentHealth);
            }
        }
    }
}
