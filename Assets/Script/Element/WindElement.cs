using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindElement : Element
{
    [field: SerializeField]
    private UnityEvent OnFire{get;set;}
    [field: SerializeField]
    private UnityEvent OnWater{get;set;}
    [field:SerializeField]
    private UnityEvent OnStone{get;set;}

    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnFire?.Invoke();
                break;
            case Conditions.Water:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnWater?.Invoke();
                break;
            case Conditions.Wind:
                hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                break;
            case Conditions.Stone:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnStone?.Invoke();
                break;
            default:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                break;
        }
    }

    public override void Bullet()
    {
        
    }
}
