using UnityEngine;

[RequireComponent(typeof(PlayerCharacterDetected))]
public class AttackZone : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    private PlayerCharacterDetected _enemyDetected;
    private Enemy _opponent;
    private float _lastAttackTime = 0f;
    private float _timePass;
    private bool _isPossibleAttack;
    private bool _isDetected;

    private void Awake()
    {
        _enemyDetected = GetComponent<PlayerCharacterDetected>();
    }

    private void Start()
    {
        _enemyDetected.OnEnemyDetected += DetectedEnemy;

        _lastAttackTime = Time.time;
    }

    private void Update()
    {
        if (_isDetected && _opponent != null)
        {
            _timePass = _lastAttackTime + _attackCooldown;

            if (Time.time >= _timePass)
            {
                _isPossibleAttack = true;
                
                _lastAttackTime = Time.time;
            }
        }
    }

    public bool GetIsPossibleAttack() => GetBoolAsTrigger(ref _isPossibleAttack);

    public Enemy GetEnemy()
    {
        return _opponent;
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }

    private void DetectedEnemy(Enemy enemy)
    {
        _enemyDetected.OnEnemyDetected -= DetectedEnemy;
        _enemyDetected.EnemyUndetected += UndetectedEnemy;

        _opponent = enemy;

        _isDetected = true;
    }

    private void UndetectedEnemy()
    {
        _enemyDetected.OnEnemyDetected += DetectedEnemy;
        _enemyDetected.EnemyUndetected -= UndetectedEnemy;

        _opponent = null;

        _isDetected = false;
    }
}
