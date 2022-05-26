using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefaultElement : Element
{
    [field: SerializeField]
    public UnityEvent OnWind;
    [field: SerializeField]
    public UnityEvent OnStone;
    public override void BulletSkill(Conditions conditions, GameObject dealer)
    {
        switch (conditions)
        {
            case Conditions.Wind:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk, dealer);
                OnWind?.Invoke();
                break;
            case Conditions.Stone:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk, dealer);
                OnStone?.Invoke();
                break;
            default:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk, dealer);
                break;
        }
    }
}
