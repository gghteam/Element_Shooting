using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    [field: SerializeField]
    [field: Range(0.1f,15f)]
    public float distance {get; set;} = 5f;

    public override bool MakeADecision()
    {
        if(Vector3.Distance(_enemyAiBrain.target.transform.position,transform.position)<distance)
        {
            if(_aIActionData.targetSpotted == false)
            {
                _aIActionData.targetSpotted = true;
            }
        }else
        {
            _aIActionData.targetSpotted = false;
        }
        return _aIActionData.targetSpotted;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
