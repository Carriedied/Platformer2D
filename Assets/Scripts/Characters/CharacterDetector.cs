using Assets.Scripts.Characters.Penguin.Interfaces;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _radiusDetected;

    private int _maxCountPerson = 5;
    private Collider2D[] _detectedColliders;

    private void Awake()
    {
        _detectedColliders = new Collider2D[_maxCountPerson];
    }

    public bool TryGetTarget(out ITarget target)
    {
        int colliderCount = Physics2D.OverlapCircleNonAlloc(transform.position, _radiusDetected, _detectedColliders, _targetMask);

        for (int i = 0; i < colliderCount; i++)
        {
            if (_detectedColliders[i].TryGetComponent<ITarget>(out target))
            {
                return true;
            }
        }

        target = null;

        return false;
    }
}
