using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField]
    private MovementDataSO movementSO;
    [SerializeField]
    protected float _currentVelocity = 3;
    protected Vector2 _movementDiraction;
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        IAgentInput input = GetComponent<IAgentInput>();
        if(input != null)
        {
            input.OnMovementKeyPressd += MoveAgent;
        }
    }
    public void MoveAgent(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            if(Vector2.Dot(movementInput,_movementDiraction) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDiraction = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }
    private float CalculateSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += movementSO.aceleration * Time.deltaTime;
        }else
        {
            _currentVelocity -= movementSO.deAceleration * Time.deltaTime;
        }
        
        return Mathf.Clamp(_currentVelocity, 0, movementSO.maxSpeed);
    }
    private void FixedUpdate() {
        rigid.velocity = _movementDiraction * _currentVelocity;    
    }
}
