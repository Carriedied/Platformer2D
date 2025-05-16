using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
public class AttackZone : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    private CharacterDetector _enemyDetected;
    private float _lastAttackTime = 0f;
    private float _timePass;

    private bool _isPossibleAttack;


    public Enemy NinjaFrog { get; private set; }

    private void Awake()
    {
        _enemyDetected = GetComponent<CharacterDetector>();
    }

    private void Start()
    {
        _lastAttackTime = Time.time;
        _timePass = Time.time;
    }

    private void Update()
    {
        if (_enemyDetected.TryGetTarget(out ITarget target))
        {
            if (target is Enemy enemy)
            {
                NinjaFrog = enemy;

                if (Time.time >= _timePass)
                {
                    _isPossibleAttack = true;
                    _lastAttackTime = Time.time;
                }

                _timePass = _lastAttackTime + _attackCooldown;
            }
        }
        else
        {
            _isPossibleAttack = false;
        }
    }

    public bool GetIsPossibleAttack() => GetBoolAsTrigger(ref _isPossibleAttack);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
