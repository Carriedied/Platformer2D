using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _radiusDetected;

    private Collider2D[] _detectedColliders;

    public ITarget DetectTargets()
    {
        _detectedColliders = Physics2D.OverlapCircleAll(transform.position, _radiusDetected, _targetMask);

        foreach (var collider in _detectedColliders)
        {
            if (collider.TryGetComponent<ITarget>(out ITarget target))
            {
                return target;
            }
        }

        return null;
    }
}
