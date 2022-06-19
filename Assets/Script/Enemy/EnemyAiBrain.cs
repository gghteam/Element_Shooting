using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAiBrain : MonoBehaviour, IAgentInput
{
    [field: SerializeField]
    public GameObject target {get;set;}
    [field: SerializeField]
    public AIState currentState {get; private set;}

    [field: SerializeField]
    public Action<Vector2> OnMovementKeyPressd { get; set;}
    [field: SerializeField]
    public Action<Vector2> OnPointerPositionChanged { get; set;}
    [field: SerializeField]
    public Action OnFireButtonPress { get; set;}
    public Action OnFireButtonReleased { get; set;}

    private AIActionData _aiActionData;
    public AIActionData AIAction => _aiActionData;
    private void Start() {
        target = GameObject.FindWithTag("Player");
    }
    private void Update() {
        if(target == null)
        {
            OnMovementKeyPressd?.Invoke(Vector2.zero);
        }
        else
        {
            currentState.UpdateState();
        }
    }
    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void SetAttackState(bool state)
    {
        _aiActionData.attack = state;
    }
    public void Move(Vector2 movementDiraction,Vector2 targetPosition)
    {
        if (GameManager.Instance.IsStopEvent) return;
        OnMovementKeyPressd?.Invoke(movementDiraction);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }
    public void ChangeToState(AIState nextState)
    {
        currentState = nextState;
    }
}
