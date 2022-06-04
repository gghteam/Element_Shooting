using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCristalElememt : Element
{
    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable?.GetHit(GameManager.Instance.PlayerATK,dealer);
                break;
            default:

                break;
        }
    }
}
