using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

public class AttackerBase : MonoBehaviour
{
    [SerializeField] private float _impactForce;

    private float _minPowerAbility = 0;

    public void Attack(IDamageable target = null, float impactForceAbility = 0)
    {
        if (impactForceAbility == _minPowerAbility)
            target.TakeDamage(_impactForce);
        else if (target != null)
            target.TakeDamage(impactForceAbility);
    }
}
