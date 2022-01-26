using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCristalElement : Element
{
    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Water:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                break;
            default:

                break;
        }
    }
}
