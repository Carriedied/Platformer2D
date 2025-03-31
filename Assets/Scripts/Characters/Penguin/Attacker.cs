using System.ComponentModel;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _impactForce;

    public void Attack(Enemy opponent)
    {
        opponent.TakeDamage(_impactForce);
    }
}
