using Assets.Scripts.Characters.Penguin.Interfaces;
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public float CurrentHitPoints { get; private set; }
    public float MaxHitPoints { get; private set; }

    public event Action OnHealthChanged;

    private void Awake()
    {
        MaxHitPoints = 100;
        CurrentHitPoints = MaxHitPoints;
    }

    public void RestoreHealth(float countMedicines)
    {
        if (countMedicines > 0)
        {
            CurrentHitPoints += countMedicines;

            OnHealthChanged?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            CurrentHitPoints -= damage;

            OnHealthChanged?.Invoke();
        }
    }
}
