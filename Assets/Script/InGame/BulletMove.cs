using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    private Element enemyElement;
    private Vector2 targetPosition;
    public Vector3 targetPostion = Vector2.zero;
    private bool _isDead;
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(_isDead)return;
        Debug.Log("HH");
        IHittable hittable = other.GetComponent<IHittable>();
        Element element = other.GetComponent<Element>();
        element.BulletSkill(GameManager.Instance.playerController.GetCondition,gameObject);
        _isDead = true;
        Spark();
        Despaw();
    }
    private void Despaw()
    {
        gameObject.SetActive(false);
    }

    private void Spark()
    {
        GameObject spark = PoolManager.Instance.GetPooledObject(2);
        ParticleSystem ps = spark.GetComponent<ParticleSystem>();
        var main = ps.main;
        var randomColors = new ParticleSystem.MinMaxGradient(GameManager.Instance.elementManager.particleG[(int)GameManager.Instance.playerController.GetCondition - 1]);
        randomColors.mode = ParticleSystemGradientMode.RandomColor;
        main.startColor = randomColors;
        spark.transform.position = gameObject.transform.position;
        spark.SetActive(true);
    }
}
