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
    private float movementSpeed;
    private float previousSpeed;
    protected Vector2 _movementDiraction;
    public Action<float> OnVelocityChange;
    protected Coroutine _KnockBackCoroution;
    private bool _isKnockBack = false;

    private void Awake() {
        movementSpeed = movementSO.maxSpeed;
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
        
        return Mathf.Clamp(_currentVelocity, 0, movementSpeed);
    }
    public void ChagedMoveAgent(float speed)
    {
        previousSpeed = movementSpeed;
        if(speed == 0)
        {
            movementSpeed = 0;
            return;
        }
        movementSpeed = movementSpeed - speed > 0 ?  movementSpeed - speed : 1; 
    }
    public void ReturnToPreviousMoveAgent()
    {
        movementSpeed = previousSpeed;
    }

    private void FixedUpdate() {
        OnVelocityChange?.Invoke(_currentVelocity);

        if (GameManager.Instance.IsStopEvent)
        {
            rigid.velocity = new Vector2(0,0);
        }
        else if(!_isKnockBack)
        rigid.velocity = _movementDiraction * _currentVelocity;    
    }

    public void KnockBack(Vector2 dir,float power,float duration)
    {
        
        if(!_isKnockBack)
        {
            _isKnockBack = true;
            _KnockBackCoroution = StartCoroutine(KnockBackCoroution(dir,power,duration));
        }
    }
    public void ResetKnockBack()
    {
        if(_KnockBackCoroution != null)
        {
            StopCoroutine(_KnockBackCoroution);
        }
        ResetKnockBackParam();
    }
    private IEnumerator KnockBackCoroution(Vector2 dir,float power,float duration)
    {
        rigid.AddForce(dir.normalized * power,ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }
    private void ResetKnockBackParam()
    {
        _currentVelocity = 0;
        rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}
