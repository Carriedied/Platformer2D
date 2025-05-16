using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHitpoints;

    public event Action HealthChanged;

    public float CurrentHitPoints { get; private set; }
    public float MaxHitPoints { get; private set; }

    private void Awake()
    {
        MaxHitPoints = _maxHitpoints;
        CurrentHitPoints = MaxHitPoints;
    }

    public void RestoreHealth(float countMedicines)
    {
        if (countMedicines > 0)
        {
            CurrentHitPoints += countMedicines;

            HealthChanged?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            CurrentHitPoints -= damage;

            HealthChanged?.Invoke();
        }
    }
}
