using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : PoolableMono
{
    [SerializeField]
    private float bulletSpeed = 0.5f;
    private Element enemyElement;
    private Vector2 targetPosition;
    public Vector3 targetPostion = Vector2.zero;
    private int _enemyLayer;
    private int _wallLayer;
    private bool _isDead;
    [SerializeField]
    private float speed = 0.1f;
    private void Awake() {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _wallLayer = LayerMask.NameToLayer("Wall");
    }
    private void Update() {
        CheckLimit();
        Move();
        AddScale();
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
        if(other.gameObject.layer == _enemyLayer)
        {
            Element element = other.GetComponent<Element>();
            element?.BulletSkill(GameManager.Instance.playerController.GetCondition,gameObject);
        }
        if(other.gameObject.layer == _wallLayer)
        {

        }
        _isDead = true;
        Spark();
        Despaw();
    }
    private void Despaw()
    {
        PoolManager.Instance.Despawn(gameObject);
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
    public override void Reset()
    {
        _isDead=false;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void AddScale()
    {
        if(transform.localScale.x < 3)
        {
            transform.localScale += new Vector3(transform.localScale.x + Time.deltaTime * speed ,transform.localScale.y + Time.deltaTime * speed, transform.localScale.z + Time.deltaTime * speed);
        }
    }
}
