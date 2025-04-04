using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _impactForce;

    public void Attack(Health player)
    {
        //player.TakeDamage(_impactForce);
    }
}
