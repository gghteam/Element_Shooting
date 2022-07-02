using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IHittable, IAgent         
{
    [SerializeField]
    private Transform playerPosition = null;

    #region Interface
    [field:SerializeField]
    public int Health {get; private set;}
    [field:SerializeField]
    public UnityEvent OnGetHit { get; set; }
    [field:SerializeField]
    public UnityEvent OnDie { get; set; }
    #endregion

    private SpriteRenderer spriteRenderer = null;
    private PlayerMove playerMove;
    private Camera Camera = null;
    private PlayerState _playerState;

    private Vector2 mousePosition = Vector2.zero;
    
    private const float coefficient = 1;
    [SerializeField]
    private float projectileSpread;

    private bool _isDead;
    private bool _isDamaged = false;
    public bool _isElement {get;set;} = false;
    public bool _isSelectElement {get;set;} = false;

    public Vector3 _hitPoint {get; private set;}

    [SerializeField]
    private Conditions condition;
    public Conditions GetCondition { get { return condition; } }

    public bool IsEnemy { get; }

    [SerializeField]
    private PlayerFly playerFly;

    [SerializeField]
    private float bulletDelay;
    [SerializeField]
    private float sphereSize;
    [SerializeField]
    private float maxFlyTime;
    private float time = 0;
    private bool isFly = false;
    private bool isKey = false;

    [Header("Fly Á¦¾î")]
    [SerializeField]
    private Slider flySlider;

    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
    }
    private void Start() {
        playerPosition = GetComponent<Transform>();
        playerMove = GetComponent<PlayerMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = _playerState.CharacterState.maxHp;
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        flySlider.maxValue = maxFlyTime;
        flySlider.value = maxFlyTime;
        StartCoroutine(Fire());
    }

    private void Update()
    {
        CheckPlayerFly();
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
        OnGetHit?.Invoke();
        if(Health <= 0)
        {
            Debug.Log("AAAAAAAAA");
            GameManager.Instance.ActiveItemInit();
            PlayerPrefs.SetInt("CurrentLevel", 1);
            playerMove.Death();
            OnDie?.Invoke();
            
            _isDead = true;
        }
        playerMove.Damaged();
        StartCoroutine(DamagedCool());
    }

    private void CheckPlayerFly()
    {
        if (!_isDead)
        {
            if (playerFly.isFly)
            {
                time += Time.deltaTime;
                flySlider.value = maxFlyTime - time;
                if (time >= maxFlyTime)
                {
                    //Debug.Log("DEATH");
                    GameManager.Instance.IsStopEvent = true;
                    GameManager.Instance.ActiveItemInit();
                    PlayerPrefs.SetInt("CurrentLevel", 1);
                    playerMove.Fall();
                    OnDie?.Invoke();
                    _isDead = true;
                    isFly = false;
                }
            }
            else
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    flySlider.value = maxFlyTime - time;
                }
            }
        }
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
            yield return new WaitForSeconds(coefficient/(float)_playerState.CharacterState.rpm);
        }
        
    }

    private void SpawnBullet() {
        
        Vector2 v2 = Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        float startRotation = angle + projectileSpread / 2f;
        float angleIncrease = projectileSpread / ((float)_playerState.CharacterState.mul - 1f);
        float randomAngle = 0;
        for (int i = 0; i < _playerState.CharacterState.mul; i++)
        {

            GameObject bullet = PoolManager.Instance.GetPooledObject(0);
            if (bullet != null)
                if (_playerState.CharacterState.mul >= 4)
                {
                    randomAngle = Random.Range(-7f, 7f);
                }
                else randomAngle = Random.Range(-3f, 3f);
            float tempRot = i == 0 ? randomAngle + angle : (startRotation - angleIncrease * i) + randomAngle;
                bullet.transform.position = playerPosition.transform.position;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, tempRot));
                bullet.GetComponent<BulletMove>().targetPostion = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));

            //Vector3 targetPos = ((Vector3)v2);

            //Vector3 startControl = (targetPos - transform.position / 4);

            bullet.SetActive(true);
            //bullet.GetComponent<BulletMove>().Bezier(targetPos, startControl);
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
            if (isKey)
            {
                GameManager.Instance.IsStopEvent = true;
                PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 1) + 1);
                GameManager.Instance.loadingController.LoadScene("InGame");
                GameManager.Instance.IsStopEvent = true;
            }
        }

        if(collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            isKey = true;
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {       
        if(collision.gameObject.CompareTag("Empty"))
        {
            //count++;
            //Debug.Log("Enter");
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
    */
}
