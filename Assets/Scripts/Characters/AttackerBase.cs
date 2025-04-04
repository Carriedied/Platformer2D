using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

public class AttackerBase : MonoBehaviour
{
    [SerializeField] private float _impactForce;

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_impactForce);
    }
}
