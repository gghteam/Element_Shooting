using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private enum EnemyState{
        NONE = 0,
        TRACKING = 1 << 0,
        DAMAGED = 2 << 1,
        SKILL = 3 << 2,
        DEAD = 4 << 3
    }
    private Transform targetPositon = null;

    private EnemyState state = EnemyState.NONE;
    private EnemyState previousState = EnemyState.NONE;
    private EnemyContoller enemyContoller = null;
    private void Start() {
        enemyContoller = GetComponent<EnemyContoller>();
        targetPositon = GameObject.FindWithTag("Player").GetComponent<Transform>();
        state = EnemyState.TRACKING;
    }
    private void Update() {
        DecideBehaviour();
    }
    private void DecideBehaviour()
    {
        switch(state)
        {
            case EnemyState.TRACKING:
                TargetTracking();
                break;
            case EnemyState.SKILL:
                break;
            case EnemyState.DAMAGED:
                break;
            default:
                break;
        }
    }
    private void TargetTracking()
    {
        Vector3 dir = targetPositon.position - transform.position;
        transform.position += dir.normalized * enemyContoller.Speed * Time.deltaTime;
    }
    public void ReturnToPreviousState()
    {
        state = previousState;
        DecideBehaviour();
    }
    public void Damaged()
    {
        previousState = state;
        state = EnemyState.DAMAGED;
    }

}
