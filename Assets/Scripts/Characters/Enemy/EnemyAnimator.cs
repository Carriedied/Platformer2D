using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int s_isRun = Animator.StringToHash("IsRun");
    private static readonly int s_isAttack = Animator.StringToHash("IsAttack");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RunAnimation(bool isAnimationWork)
    {
        _animator.SetBool(s_isRun, isAnimationWork);
    }

    public void StartAttackAnimation()
    {
        _animator.SetBool(s_isAttack, true);
    }

    public void AttackAnimationEnd()
    {
        _animator.SetBool(s_isAttack, false);
    }
}
