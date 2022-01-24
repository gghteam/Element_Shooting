
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            if(!transform.GetChild(1).gameObject.activeSelf)
            {
                GameObject target = transform.GetChild(1).gameObject;
                target.GetComponent<Canvas>().worldCamera = GameManager.Instance.camera.gameObject.GetComponent<Camera>();
                target.transform.GetChild(0).gameObject.GetComponent<Slider>().maxValue = currentHp;
                target.transform.GetChild(0).gameObject.GetComponent<Slider>().value = currentHp;
                target.transform.GetChild(0).transform.GetChild(3).GetComponent<Image>().sprite = GameManager.Instance.uiManager.elementMark[(int)condition - 1];
                target.SetActive(true);
            }
            Damaged();
            DamagePopup.Create(transform.position, GameManager.Instance.PlayerInfo.atk, false);
            PoolManager.Instance.Despawn(col.gameObject);
        }
    }
    private void Damaged(){
        isDamaged = true;
        ChangeHp(GameManager.Instance.PlayerInfo.atk);
        int k = CheckHp();
        if (k == 0) return;
        StartCoroutine(OnDamagedAnimation());
    }

    public void ChangeHp(float decrease)
    {
        GameObject target = transform.GetChild(1).gameObject;
        currentHp -= (int)decrease;
        target.transform.GetChild(0).gameObject.GetComponent<Slider>().value = currentHp;
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

        yield return new WaitForSeconds(2f);

        spriteRenderer.color = new Color(1, 1, 1, 1);
        collider.enabled = true;
        transform.GetChild(1).gameObject.SetActive(false);
        PoolManager.Instance.Despawn(gameObject);
        
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
