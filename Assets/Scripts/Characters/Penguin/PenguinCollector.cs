using Assets.Scripts.Characters.Penguin.Interfaces;
using Assets.Scripts.Characters.Penguin;
using UnityEngine;

[RequireComponent(typeof(WalletPenguin))]
[RequireComponent(typeof(Health))]
public class PenguinCollector : MonoBehaviour
{
    [SerializeField] private float _countMedicines;

    private WalletPenguin _purse;
    private Health _health;

    private void Awake()
    {
        _purse = GetComponent<WalletPenguin>();
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            var visitor = new CollectorVisitor(_purse, _health, _countMedicines);
            collectible.Accept(visitor);
        }
    }
}
