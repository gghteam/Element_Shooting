using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAiBrain _enemyBrain = null;
    [SerializeField]
    private List<AIAction> _actions = null;
    [SerializeField]
    private List<AITransition> _transitions = null;

    private void Awake() {
        _enemyBrain = transform.GetComponentInParent<EnemyAiBrain>();
    }

    public void UpdateState()
    {
        foreach(AIAction action in _actions)
        {
            action.TakeAction();
        }
        
        foreach(AITransition transition in _transitions)
        {
            bool result = false;
            foreach(AIDecision decision in transition.decisions)
            {
                result = decision.MakeADecision();
                if(!result) break;
            }

            if(result)
            {
                if(transition.positiveResult != null)
                {
                    _enemyBrain.ChangeToState(transition.positiveResult);
                    return;
                }
            }else
            {
                if(transition.negativeResult != null)
                {
                    _enemyBrain.ChangeToState(transition.negativeResult);
                    return;
                }
            }
        }
    }
}
