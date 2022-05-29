using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IHittable, IAgent         
{
    [SerializeField]
    private Transform playerPosition = null;
    [field:SerializeField]
    public int Health {get; private set;}
    [field:SerializeField]
    public UnityEvent OnGetHit { get; set; }
    [field:SerializeField]
    public UnityEvent OnDie { get; set; }
    private PlayerMove playerMove;
    private Camera Camera = null;
    private Vector2 mousePosition = Vector2.zero;
    private SpriteRenderer spriteRenderer = null;
    private const float coefficient = 1;
    [SerializeField]
    private float projectileSpread;
    private bool _isDead;
    [SerializeField]
    private Conditions condition;
    public bool _isElement {get;set;} = false;
    public bool _isSelectElement {get;set;} = false;
    private bool _isDamaged = false;
    public Vector3 _hitPoint {get; private set;}
    public Conditions GetCondition { get { return condition; } }
    [SerializeField]
    private float bulletDelay;
    [SerializeField]
    private float sphereSize;
    [SerializeField]
    private float maxFlyTime;
    private float time = 0;
    private bool isFly = false;
    private int count = 0;
    private void Start() {
        playerMove = GetComponent<PlayerMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = GameManager.Instance.PlayerInfo.maxHp;
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        StartCoroutine(Fire());
    }

    private void Update()
    {
        if (!_isDead)
        {
            if (isFly)
            {
                time += Time.deltaTime;
                if (time > maxFlyTime)
                {
                    Debug.Log("DEATH");
                    playerMove.Death();
                    OnDie?.Invoke();
                    _isDead = true;
                    isFly = false;
                }
            }
            else
            {
                if (time > 0)
                    time -= Time.deltaTime;
            }
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (GameManager.Instance.IsStopEvent) return;
        if (_isDead) return;
        if (_isDamaged) return;
        _isDamaged = true;
        StartCoroutine(GameManager.Instance.camera.Shake(0.2f, 0.3f));
        GameManager.Instance.ChangeHealthValue(-damage);
        Health -= damage;
        EventManager.TriggerEvent(EventManager.EventName.PLAYER_DAMAGED);
        Debug.Log(GameManager.Instance.PlayerInfo.hp);
        OnGetHit?.Invoke();
        if(Health <= 0)
        {
            playerMove.Death();
            OnDie?.Invoke();
            _isDead = true;
        }
        playerMove.Damaged();
        StartCoroutine(DamagedCool());
    }
    private IEnumerator DamagedCool()
    {
        yield return new WaitForSeconds(0.5f);
        _isDamaged = false;
    }
    private IEnumerator Fire()
    {
        while(true)
        {
            yield return null;
            if (GameManager.Instance.IsStopEvent) continue;
            if (PlayerPrefs.GetInt("TURORIAL", 1) == 1)
            {
                if (GameManager.Instance.IsStopEvent || GameManager.Instance.dialogueManager.IsDialogue) continue;
            }
            if(_isElement) continue;
            if(_isSelectElement) continue;

            if(!Input.GetMouseButton(0)) {
                playerMove.UnAttack();
                continue;
            }
            SoundManager.Instance.AttackSound();
            playerMove.Attack();
            Invoke("SpawnBullet", bulletDelay);
            yield return new WaitForSeconds(coefficient/(float)GameManager.Instance.PlayerInfo.rpm);
        }
        
    }

    private void SpawnBullet() {
        
        Vector2 v2 = Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        float startRotation = angle + projectileSpread / 2f;
        float angleIncrease = projectileSpread / ((float)GameManager.Instance.PlayerInfo.mul - 1f);
        float randomAngle = 0;
        for (int i = 0; i < GameManager.Instance.PlayerInfo.mul; i++)
        {
            GameObject bullet = PoolManager.Instance.GetPooledObject(0);
            if (bullet != null)
                
                if (GameManager.Instance.PlayerInfo.rpm >= 6)
                {
                    randomAngle = Random.Range(-5f, 5f);
                }
                else randomAngle = 0;
                float tempRot = i == 0 ? randomAngle + angle : (startRotation - angleIncrease * i) + randomAngle;
                bullet.transform.position = playerPosition.transform.position;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, tempRot));
                bullet.GetComponent<BulletMove>().targetPostion = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));
                bullet.SetActive(true);
                int rX = Random.Range(-20, 20);
                int rY = Random.Range(-20, 20);
                GameManager.Instance.rebound.StartBandong(rX, rY);
                StartCoroutine(GameManager.Instance.camera.Shake(0.02f, 0.1f));
                ChangeBulletSprite(bullet);
            
        }
    }
    private void ChangeBulletSprite(GameObject bullet)
    {
        bullet.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.elementManager.bulletSprite[(int)condition - 1];
        bullet.GetComponent<Animator>().Play(GameManager.Instance.elementManager.animationString[(int)condition - 1]);
    }

    public void ChangeCondition(Conditions change)
    {
        condition = change;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End_Map"))
        {
            GameManager.Instance.loadingController.LoadScene("InGame");
            GameManager.Instance.IsStopEvent = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {       
        if(collision.gameObject.CompareTag("Empty"))
        {
            //count++;
            Debug.Log("Enter");
            isFly = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Empty"))
        {
            Debug.Log("Exit");
            isFly = false;
        }
    }
}
