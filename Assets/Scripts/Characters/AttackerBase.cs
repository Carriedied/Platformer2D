using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

public class AttackerBase : MonoBehaviour
{
    [SerializeField] private float _impactForce;
    [SerializeField] private float _attackCooldown;

    private float _timePass;
    private float _lastAttackTime;

    private bool _isPossibleAttack;

    private void Start()
    {
        _lastAttackTime = Time.time;
        _timePass = Time.time;
    }

    private void FixedUpdate()
    {
        _timePass = _lastAttackTime + _attackCooldown;

        if (Time.time >= _timePass)
        {
            _isPossibleAttack = true;

            _lastAttackTime = Time.time;
        }
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_impactForce);
    }

    public bool GetIsAttack() =>
        GetBoolAsTrigger(ref _isPossibleAttack);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
