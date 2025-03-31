using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PenguinAnimator))]
[RequireComponent(typeof(AttackZone))]
[RequireComponent(typeof(HealthCharacter))]
[RequireComponent(typeof(Attacker))]
public class Penguin : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    private Mover _mover;
    private InputReader _inputReader;
    private PenguinAnimator _playerAnimator;
    private AttackZone _attackZone;
    private HealthCharacter _healthPlayer;
    private Attacker _attacker;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PenguinAnimator>();
        _attackZone = GetComponent<AttackZone>();
        _healthPlayer = GetComponent<HealthCharacter>();
        _attacker = GetComponent<Attacker>();
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

        if (_inputReader.GetIsJump() && _groundDetector.GetIsGround())
            _mover.Jump();

        if (_inputReader.GetIsAttack() && _attackZone.GetIsPossibleAttack())
        {
            Enemy opponent = _attackZone.GetEnemy();

            if (opponent != null)
            {
                _playerAnimator.StartAttackAnimation();
                _mover.AttackMove(opponent);
                _attacker.Attack(opponent);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _healthPlayer.TakeAwayHealth(damage);
    }
}
