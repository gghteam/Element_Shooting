using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimator
{
    protected EnemyAiBrain _enemyAIBrain;
    protected readonly int _jumpdownHasStr = Animator.StringToHash("Jumpdown");

    protected override void ChildAwake()
    {
        _enemyAIBrain = transform.parent.GetComponent<EnemyAiBrain>();
    }

    public void SetEndOfAttackAnimation()
    {
        _enemyAIBrain.SetAttackState(false);
    }

    public void PlayAttackAnimation()
    {
        _agentAnimator.SetTrigger(_attackHashStr);
    }

    public void PlayJumpdownAnimation()
    {
        _agentAnimator.SetTrigger(_jumpdownHasStr);
    }
}
