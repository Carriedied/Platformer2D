using Assets.Scripts.Characters.Indicators;
using System;
using UnityEngine;

[RequireComponent(typeof(VampirismAbility))]
public class AttackCooldown : MonoBehaviour
{
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _abilityDelay = 4;

    private VampirismAbility _vampirismAbility;

    private float _timePassAttack;
    private float _timePassAbility;

    private float _lastAttackTime;
    private float _lastUseAbilityTime;

    private bool _isPossibleAttack = false;
    private bool _isPossibleUseAbility = false;
    private bool _isAbilityBeingUsed = false;

    private void Awake()
    {
        _vampirismAbility = GetComponent<VampirismAbility>();
    }

    private void Start()
    {
        _lastAttackTime = Time.time;
        _lastUseAbilityTime = Time.time;

        _vampirismAbility.OnAbilityCompleted += EndUsageAbility;
    }

    private void FixedUpdate()
    {
        UpdateCooldown();

        if (!_isAbilityBeingUsed)
            UpdateCooldownAbility();
    }

    public float GetAbilityDelay()
    {
        return _abilityDelay;
    }

    private void EndUsageAbility()
    {
        _isAbilityBeingUsed = false;

        _lastUseAbilityTime = Time.time;

        _vampirismAbility.OnAbilityCompleted -= EndUsageAbility;
    }

    private void UpdateCooldown()
    {
        _timePassAttack = _lastAttackTime + _attackDelay;

        if (Time.time >= _timePassAttack)
        {
            _isPossibleAttack = true;
            _lastAttackTime = Time.time;
        }
    }

    private void UpdateCooldownAbility()
    {
        _timePassAbility = _lastUseAbilityTime + _abilityDelay;

        if (Time.time >= _timePassAbility)
        {
            _isPossibleUseAbility = true;

            _isAbilityBeingUsed = true;
            _vampirismAbility.OnAbilityCompleted += EndUsageAbility;
        }
    }

    public bool GetIsUseAbility() =>
        GetBoolAsTrigger(ref _isPossibleUseAbility);

    public bool GetIsAttack() =>
        GetBoolAsTrigger(ref _isPossibleAttack);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
