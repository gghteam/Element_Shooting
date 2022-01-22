using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData _aIActionData;
    protected AIMovementData _aIMovementData;
    protected EnemyAiBrain _enemyAiBrain;

    private void Awake() {
        _enemyAiBrain = transform.GetComponentInParent<EnemyAiBrain>();
        _aIActionData = _enemyAiBrain.transform.GetComponentInChildren<AIActionData>();
        _aIMovementData = _enemyAiBrain.transform.GetComponentInChildren<AIMovementData>();

        ChildAwake();
    }
    protected virtual void ChildAwake()
    {

    }

    public abstract bool MakeADecision();
}
