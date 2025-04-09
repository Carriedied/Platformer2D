using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private float _attackZone;

    public bool GetIsAttakZone(Penguin attacker)
    {
        float distancePlayer = Mathf.Abs(attacker.transform.position.x - transform.position.x);

        return distancePlayer <= _attackZone;
    }
}