using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _aIMovementData.dir = Vector2.zero;
        _aIMovementData.pointOfInterest = _enemyAiBrain.target.transform.position;
        _enemyAiBrain.Move(_aIMovementData.dir,_aIMovementData.pointOfInterest);
    }
}
