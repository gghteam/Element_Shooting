
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyContoller : MonoBehaviour
{
    [SerializeField]
    private Character enemyInfo;
    [SerializeField]
    private GameObject circle;

    public GameObject getcircle { get { return circle; } }
    public Character getInfo { get { return enemyInfo; } }
    public int currentHp { get; private set; }
    private EnemyMove enemyMove = null;

    private EnemyContoller enemyContoller = null;

    private new Collider2D collider = null;
    public Rigidbody2D rigid { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    private bool isDamaged = false;
    public bool isElement = false;

    [SerializeField]
    private Conditions condition;

    public Conditions getCondition
    {
        get
        {
            return condition;
        }
    }
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMove = GetComponent<EnemyMove>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        enemyContoller = GetComponent<EnemyContoller>();
        currentHp = enemyInfo.hp;
    }
    public int Atk { 
        get{
            return enemyInfo.atk;
        }
    }
    public float Speed {
        get{
            return enemyInfo.speed;
        }
    }
    private void Init()
    {
        isDamaged = false;
        currentHp = enemyInfo.hp;
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerBullet"))
        {
            if(isDamaged)return;
            Damaged();
            PoolManager.Instance.Despawn(col.gameObject);
        }
        if (col.gameObject.CompareTag("Range"))
        {
            if (GameManager.Instance.elementManager.isCheck[0] == true)
            {
                isElement = false;
                StartCoroutine(GameManager.Instance.elementManager.FireBullet(enemyContoller));
                GameManager.Instance.elementManager.WindBullet(enemyContoller);
            }
            else if (GameManager.Instance.elementManager.isCheck[1] == true)
            {
                StartCoroutine(GameManager.Instance.elementManager.WaterBullet(enemyContoller));
                GameManager.Instance.elementManager.WindBullet(enemyContoller);
            }
        }
    }
    private void Damaged(){
        isDamaged = true;
        ChangeHp(GameManager.Instance.PlayerInfo.atk);
        int k = CheckHp();
        if (k == 0) return;
        GameManager.Instance.elementManager.BulletSkill(gameObject);
        StartCoroutine(Spark());
        StartCoroutine(OnDamagedAnimation());

    }

    public void ChangeHp(float decrease)
    {
        currentHp -= (int)decrease;
    }

    private IEnumerator OnDamagedAnimation()
    {
        enemyMove.Damaged();
        spriteRenderer.color = new Color(1f,1f,1f,0.5f);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = new Color(1f,1f,1f,0.5f);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        enemyMove.ReturnToPreviousState();
        isDamaged = false;
    }
    public IEnumerator OnDead(){
        GameManager.Instance.ChangeExpValue(enemyInfo.exp);

        spriteRenderer.color = new Color(1, 1, 1, 0);
        collider.enabled = false;
        gameObject.SetActive(false);

        GameObject spark = PoolManager.Instance.GetPooledObject(2);
        spark.transform.position = gameObject.transform.position;
        spark.SetActive(true);

        yield return new WaitForSeconds(2f);

        PoolManager.Instance.Despawn(spark);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        collider.enabled = true;
        PoolManager.Instance.Despawn(gameObject);
        
    }

    private IEnumerator Spark()
    {
        GameObject spark = PoolManager.Instance.GetPooledObject(2);
        ParticleSystem ps = spark.GetComponent<ParticleSystem>();
        var main = ps.main;
        var randomColors = new ParticleSystem.MinMaxGradient (GameManager.Instance.elementManager.particleG[(int)GameManager.Instance.playerController.GetCondition - 1]);
        randomColors.mode = ParticleSystemGradientMode.RandomColor;
        main.startColor = randomColors;
        spark.transform.position = gameObject.transform.position;
        spark.SetActive(true);

        yield return new WaitForSeconds(2f);

        PoolManager.Instance.Despawn(spark);
    }

    public int CheckHp()
    {
        if (currentHp <= 0)
        {
            StartCoroutine(OnDead());
            return 0;
        }
        return 1;
    }
    public void ChangeSpeed(float data)
    {
        enemyInfo.speed = data;
    }
}
