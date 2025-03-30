using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode JumpKey = KeyCode.Space;
    public const KeyCode AttackKey = KeyCode.F;

    private bool _isJump;
    private bool _isAttack;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKey(JumpKey))
            _isJump = true;

        if (Input.GetKey(AttackKey))
            _isAttack = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
