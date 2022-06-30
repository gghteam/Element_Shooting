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
    private float axis =0; //가리키는 방향의 회전 축
    [SerializeField]
    private float _degree = 0;

    private IHittable _hittable;

    private float runningTime;

    private LayerMask _layer;

    private void Awake()
    {
        _layer = LayerMask.GetMask("Wall");
        _hittable = GameManager.Instance.playerController.transform.GetComponent<IHittable>();
        runningTime += _degree * Mathf.Deg2Rad;
    }

    private void Update()
    {
        ShowLaser();
    }

    private void ShowLaser()
    {
        //원을 그리며 돌아가는 벡터
        runningTime += Time.deltaTime * _moveSpeed;
        float x = Mathf.Cos(runningTime);
        float y = Mathf.Sin(runningTime);
        Vector2 vec = new Vector2(x, y);

        //맞은 벽의 위치를 구하는 레이
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, vec.normalized,100,_layer);

        Vector3 endPos = (Vector3)hit.point - transform.position; //맞은 위치의 벡터
        Vector3 quaternion = Quaternion.Euler(new Vector3(0, 0, axis)) * endPos; //그위치에 쿼티니언

        Quaternion rotate = Quaternion.LookRotation(Vector3.forward, quaternion); //회전

        transform.localScale = new Vector2(endPos.magnitude, 1);
        transform.rotation = rotate;

        CheckRayHitPlayer(endPos);
        Debug.DrawRay(transform.position, vec.normalized, Color.red);
    }

    private void CheckRayHitPlayer(Vector2 vec)
    {
        if(Physics2D.Linecast(transform.position,vec,1 << LayerMask.NameToLayer("Player")))
        {
            _hittable?.GetHit(2, gameObject);
        }
    }
}
