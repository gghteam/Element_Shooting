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
    private float axis =0; //����Ű�� ������ ȸ�� ��
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
        //���� �׸��� ���ư��� ����
        runningTime += Time.deltaTime * _moveSpeed;
        float x = Mathf.Cos(runningTime);
        float y = Mathf.Sin(runningTime);
        Vector2 vec = new Vector2(x, y);

        //���� ���� ��ġ�� ���ϴ� ����
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, vec.normalized,100,_layer);

        Vector3 endPos = (Vector3)hit.point - transform.position; //���� ��ġ�� ����
        Vector3 quaternion = Quaternion.Euler(new Vector3(0, 0, axis)) * endPos; //����ġ�� ��Ƽ�Ͼ�

        Quaternion rotate = Quaternion.LookRotation(Vector3.forward, quaternion); //ȸ��

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
