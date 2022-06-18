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
                hittable.GetHit(GameManager.Instance.PlayerATK, dealer);
                break;
            case Conditions.Water:
                hittable.GetHit(GameManager.Instance.PlayerATK, dealer);
                break;
            case Conditions.Wind:
                hittable.GetHit(GameManager.Instance.PlayerATK, dealer);
                break;
            case Conditions.Stone:
                hittable.GetHit(GameManager.Instance.PlayerATK, dealer);
                break;
        }
    }
}
