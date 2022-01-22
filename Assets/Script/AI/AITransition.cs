using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [field: SerializeField]
    public List<AIDecision> decisions {get; set;}
    [field: SerializeField]
    public AIState positiveResult {get; set;}
    [field: SerializeField]
    public AIState negativeResult {get; set;}

    private void Awake() {
        decisions.Clear();
        GetComponents<AIDecision>(decisions);
    }
}
