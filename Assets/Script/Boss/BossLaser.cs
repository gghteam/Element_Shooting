using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [SerializeField]
    private Sprite _laserSprite;
    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private LayerMask _layer;

    private float runningTime;

    private void Update()
    {
        ShowLaser();
    }

    private void ShowLaser()
    {
        runningTime += Time.deltaTime * _moveSpeed;
        float x = Mathf.Cos(runningTime);
        float y = Mathf.Sin(runningTime);
        Vector2 vec = new Vector2(x, y);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, vec.normalized);
        Vector2 distance = hit.transform.position - transform.position;
        Debug.DrawRay(transform.position, vec.normalized * distance.x, Color.red) ;

        Debug.Log(hit.);
        //Vector2 distance = hit.transform.position - transform.position;
        //Debug.Log(hit.transform.position);
        //Debug.Log(distance);
        //transform.localScale = new Vector3(distance.x / 2, 1, 1);
    }
}
