using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float _maxHealth = 100;

    public float HitPoints { get; private set; }

    private void Awake()
    {
        HitPoints = _maxHealth;
    }

    public void RestoreHealth(float countMedicines)
    {
        HitPoints += countMedicines;
    }

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
    }
}
