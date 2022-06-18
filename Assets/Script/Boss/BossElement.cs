using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElement : Element
{
    protected Boss _boss;
    protected override void ChildAwake()
    {
        _boss = GetComponent<Boss>();
        hittable = _boss.GetComponent<IHittable>();
    }
    public override void BulletSkill(Conditions conditions, GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable.GetHit(1, dealer);
                break;
            case Conditions.Water:
                hittable.GetHit(1, dealer);
                break;
            case Conditions.Wind:
                hittable.GetHit(1, dealer);
                break;
            case Conditions.Stone:
                hittable.GetHit(1, dealer);
                break;
        }
    }
}
