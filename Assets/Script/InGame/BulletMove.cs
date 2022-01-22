using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    private Vector2 targetPosition;
    public Vector3 targetPostion = Vector2.zero;
    private void Update() {
        CheckLimit();
        Move();
    }
    private void Move()
    {
        transform.position = transform.position + (targetPostion.normalized * bulletSpeed * Time.deltaTime); 
    }
    private void CheckLimit()
    {
        if(transform.position.x > GameManager.Instance.maxPosition.x||transform.position.x < GameManager.Instance.minPosition.x)
        {
            Despaw();
        }
        if(transform.position.y > GameManager.Instance.maxPosition.y||transform.position.y < GameManager.Instance.minPosition.y)
        {
            Despaw();
        }
    }
    private void Despaw()
    {
        gameObject.SetActive(false);
    }
}
