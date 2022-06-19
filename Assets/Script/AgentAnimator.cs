using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimator : MonoBehaviour
{
    protected Animator _agentAnimator;

    private readonly int _walkHashStr = Animator.StringToHash("Walk");
    private readonly int _deathHashStr = Animator.StringToHash("Death");
    protected readonly int _attackHashStr = Animator.StringToHash("Attack");

    private void Awake()
    {
        _agentAnimator = GetComponent<Animator>();
        GetComponentInParent<AgentMovement>().OnVelocityChange += AnimatePlayer;
        GetComponentInParent<Enemy>().OnAttackAnimation += SetAttackAnimaiton;
        GetComponentInParent<Enemy>().OnDieAnimaiton += PlayDeathAnimation;
        ChildAwake();
    }
    protected virtual void ChildAwake()
    {

    }
    public void SetAttackAnimaiton()
    {
        _agentAnimator.SetBool(_walkHashStr, false);
        _agentAnimator.SetTrigger(_attackHashStr);
    }

    public void SetWalkAnimation(bool value)
    {
        _agentAnimator.SetBool(_walkHashStr, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);   
    }

    public void PlayDeathAnimation()
    {
        _agentAnimator.SetTrigger(_deathHashStr);
    }
}
