using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
public class AttackZone : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    private CharacterDetector _enemyDetected;

    private ITarget _opponent;
    private Enemy _ninjaFrog;
    private float _lastAttackTime = 0f;
    private float _timePass;

    public bool IsPossibleAttack { get; private set; }

    private void Awake()
    {
        _enemyDetected = GetComponent<CharacterDetector>();
    }

    private void Start()
    {
        _lastAttackTime = Time.time;
    }

    private void Update()
    {
        _opponent = _enemyDetected.DetectTargets();

        if (_opponent is Enemy enemy)
        {
            _ninjaFrog = enemy;

            if (Time.time >= _timePass)
            {
                IsPossibleAttack = true;
                
                _lastAttackTime = Time.time;
            }

            _timePass = _lastAttackTime + _attackCooldown;
        }

        if (_opponent == null)
        {
            IsPossibleAttack = false;
        }
    }

    public Enemy GetEnemy()
    {
        return _ninjaFrog;
    }
}
