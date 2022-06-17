using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : PoolableMono
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;

    private Vector2 _dir = Vector2.zero;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }
    public void SetBullet(Vector2 vec,Vector3 rot ,int spriteNum)
    {
        _dir = vec;
        transform.Rotate(rot);
        _spriteRenderer.sprite = _sprites[spriteNum];
    }


    private void Move()
    {
        transform.Translate(_dir * _moveSpeed * Time.deltaTime);
    }

    public override void Reset()
    {
        
    }
}
