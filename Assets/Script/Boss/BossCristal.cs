using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCristal : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _distance = 2;

    float runningTime = 0;
    private Boss _boss;
    private void Awake()
    {
        _boss = GetComponentInParent<Boss>();
    }
    private void Update()
    {
        NormalMove();
    }

    private void NormalMove()
    {
        runningTime += Time.deltaTime * _moveSpeed;
        float x = Mathf.Cos(runningTime) * _distance;
        float y = Mathf.Sin(runningTime) * _distance;
        transform.position = new Vector2(x, y) + new Vector2(0, 1);
    }
}
