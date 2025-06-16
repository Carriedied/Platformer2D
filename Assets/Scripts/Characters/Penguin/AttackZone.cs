using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

[RequireComponent(typeof(CharacterDetector))]
[RequireComponent(typeof(Penguin))]
public class AttackZone : MonoBehaviour
{
    private CharacterDetector _enemyDetected;
    private Penguin _player;

    public Enemy NinjaFrog { get; private set; }

    private void Awake()
    {
        _enemyDetected = GetComponent<CharacterDetector>();
        _player = GetComponent<Penguin>();
    }

    private void Update()
    {
        if (_enemyDetected.TryGetTarget(out ITarget target))
        {
            if (target is Enemy enemy)
            {
                NinjaFrog = enemy;
                _player.ChangeDetectedEnemy(true);
            }
        }
        else
        {
            _player.ChangeDetectedEnemy(false);
        }
    }
}
