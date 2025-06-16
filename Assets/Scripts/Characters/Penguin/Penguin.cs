using Assets.Scripts.Characters.Indicators;
using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(AttackZone))]
[RequireComponent(typeof(AttackerBase))]
[RequireComponent(typeof(AttackCooldown))]
[RequireComponent(typeof(VampirismAbility))]
public class Penguin : MonoBehaviour, ITarget
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PenguinAnimator _playerAnimator;
    [SerializeField] private SmoothSliderAbility _abilitySlider;

    private Mover _mover;
    private InputReader _inputReader;
    private AttackZone _attackZone;
    private AttackerBase _attackerBase;
    private AttackCooldown _attackCooldown;
    private VampirismAbility _vampirismAbility;
    private Enemy _opponent;
    Health _opponentHealth;

    public bool IsDetectedEnemy { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _attackZone = GetComponent<AttackZone>();
        _attackerBase = GetComponent<AttackerBase>();
        _attackCooldown = GetComponent<AttackCooldown>();
        _vampirismAbility = GetComponent<VampirismAbility>();
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

        if (IsDetectedEnemy)
        {
            _opponent = _attackZone.NinjaFrog;

            if (_opponent != null)
            {
                _opponentHealth = _opponent.GetHealthComponent();

                if (_opponentHealth != null)
                {
                    if (_inputReader.GetIsAttack() && _attackCooldown.GetIsAttack())
                    {
                        _playerAnimator.StartAttackAnimation();

                        _mover.AttackMove(_opponent);

                        _attackerBase.Attack(_opponentHealth);

                        _opponent = null;
                        _opponentHealth = null;
                    }
                }
            }
        }

        if (_inputReader.GetIsAbilityActivated() && _attackCooldown.GetIsUseAbility())
        {
            _vampirismAbility.OnAbilityCompleted += EndAbilityAnimation;

            _vampirismAbility.UseAbility(_opponentHealth);

            _abilitySlider.StartAbilityUsage(_vampirismAbility.GetAbilityUsageTime());
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

    private void EndAbilityAnimation()
    {
        _vampirismAbility.OnAbilityCompleted -= EndAbilityAnimation;

        _abilitySlider.StartAbilityCooldown(_attackCooldown.GetAbilityDelay());
    }
}
