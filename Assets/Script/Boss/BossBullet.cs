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

    [SerializeField]
    private LayerMask _enemyLayer;
    [SerializeField]
    private LayerMask _wallLayer;

    private Vector2 _dir = Vector2.right;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable.GetHit(1, gameObject);
            Debug.Log("Player Hit!");
        }
        if (collision.gameObject.layer == _wallLayer)
        {

        }
        PoolManager.Instance.Despawn(this.gameObject);
    }

    public override void Reset()
    {
        _dir = Vector2.zero;
        transform.Rotate(Vector3.zero);
    }
}
