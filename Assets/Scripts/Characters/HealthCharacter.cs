using UnityEngine;

public class HealthCharacter : MonoBehaviour
{
    public float Health { get; private set; }

    private void Awake()
    {
        Health = 100;
    }

    public void TakeAwayHealth(float damage)
    {
        Health -= damage;
    }

    public void RestoreHealth(float countMedicines)
    {
        Health += countMedicines;
    }
}
