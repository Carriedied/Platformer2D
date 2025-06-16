using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode JumpKey = KeyCode.Space;
    public const KeyCode AttackKey = KeyCode.F;
    public const KeyCode AbilityKey = KeyCode.G;

    private bool _isJump;
    private bool _isAttack;
    private bool _isAbilityActivated;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKey(JumpKey))
            _isJump = true;

        if (Input.GetKey(AttackKey))
            _isAttack = true;

        if (Input.GetKey(AbilityKey))
            _isAbilityActivated = true;
    }

    public bool GetIsJump() => 
        GetBoolAsTrigger(ref _isJump);

    public bool GetIsAttack() => 
        GetBoolAsTrigger(ref _isAttack);

    public bool GetIsAbilityActivated() =>
        GetBoolAsTrigger(ref _isAbilityActivated);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
