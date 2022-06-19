using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapElement : Element
{
    protected FireTrap _fireTrap;
    private Animator _animator;

    protected readonly int _OffHashStr = Animator.StringToHash("Off");
    protected readonly int _OnHashStr = Animator.StringToHash("On");
    protected override void ChildAwake()
    {
        _fireTrap = GetComponent<FireTrap>();
        _animator = GetComponent<Animator>();
    }
    public override void BulletSkill(Conditions conditions, GameObject dealer)
    {
        if(conditions == Conditions.Water)
        {
            _fireTrap.IsFire = false;
            _animator.SetTrigger(_OffHashStr);
        }
        if(conditions == Conditions.Fire)
        {
            _fireTrap.IsFire = true;
            _animator.SetTrigger(_OnHashStr);
        }
    }
}
