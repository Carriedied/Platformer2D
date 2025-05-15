using UnityEngine;

public abstract class HealthBarBase : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health HitPoints => _health;

    protected virtual void OnEnable()
    {
        _health.OnHealthChanged += UpdateUI;
    }

    protected virtual void OnDisable()
    {
        _health.OnHealthChanged += UpdateUI;
    }

    protected abstract void UpdateUI();
}