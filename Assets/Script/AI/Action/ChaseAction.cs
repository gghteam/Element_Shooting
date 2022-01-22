using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 direction = _enemyAiBrain.target.transform.position - transform.position;
        _aIMovementData.dir = direction.normalized;
        _aIMovementData.pointOfInterest = _enemyAiBrain.target.transform.position;
        _enemyAiBrain.Move(_aIMovementData.dir, _aIMovementData.pointOfInterest);
    }
}
