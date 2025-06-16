using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AttackerBase))]
public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private float _powerAbility;

    private AttackerBase _attackerBase;

    private float _abilityUsageTime = 6;
    private Coroutine _abilityCoroutine;

    public event Action OnAbilityCompleted;

    private void Awake()
    {
        _attackerBase = GetComponent<AttackerBase>();
    }

    public float GetAbilityUsageTime()
    {
        return _abilityUsageTime;
    }

    public void UseAbility(IDamageable target)
    {
        if (_abilityCoroutine == null)
        {
            _abilityCoroutine = StartCoroutine(ApplyVampirism(target));
        }
    }

    private IEnumerator ApplyVampirism(IDamageable target)
    {
        float damagePerInterval = _powerAbility / _abilityUsageTime;
        float interval = 1f;

        for (float elapsed = 0; elapsed <= _abilityUsageTime; elapsed += interval)
        {
            _attackerBase.Attack(target, damagePerInterval);

            yield return new WaitForSeconds(interval);
        }

        OnAbilityCompleted?.Invoke();

        _abilityCoroutine = null;
    }
}
