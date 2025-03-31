using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _impactForce;

    public void Attack(Penguin player)
    {
        player.TakeDamage(_impactForce);
    }
}
