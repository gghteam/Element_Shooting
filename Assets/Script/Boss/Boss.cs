using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using static DefineCS;
using Random = UnityEngine.Random;

enum BossState
{
    Idle,
    Attack,
    RangeOn,
    Range,
    RangeOff,
    MoveDown,
    MoveUp,
    Death
}
public class Boss : MonoBehaviour, IHittable,IAgent
{
    [SerializeField]
    private BossDataSO _bossDataSO;

    [SerializeField]
    private GameObject _bossBulletPrefab;

    [SerializeField]
    private GameObject _EndPanel;

    private BossState _currentState = BossState.Idle;
    private BossState _beforeState = BossState.Idle;

    private Transform _targetTrm = null;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audio;
    private BossHp _bossHp;

    private BossLaser _leftLaser;
    private BossLaser _rightLaser;

    private readonly int _attackHashStr = Animator.StringToHash("Attack");
    private readonly int _rangeAttackHashStr = Animator.StringToHash("RangeAttack");
    private readonly int _moveDownHashStr = Animator.StringToHash("MoveDown");
    private readonly int _moveUpHashStr = Animator.StringToHash("MoveUp");

    private bool _isAttack = false;
    private bool _isTwoPhase = false;
    private bool _isTwoPhaseStar = false;
    private bool _isFaceRight = false;

    #region Inferface
    public Vector3 _hitPoint { get; set; }

    public int Health { get; set; }

    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy { get; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;

        float hp = (float)Health / _bossDataSO.health;
        _bossHp.SetValue(hp);

        DamagePopup.Create(transform.localPosition, -damage, false);

        if (Health <= 0)
        {

            SceneManager.LoadScene("End");
            Debug.Log("Boss Die");
        }

        if(Health<_bossDataSO.health/4)
        {
            _isTwoPhase = true;
        }
    }
    #endregion


    private void Awake()
    {
        Health = _bossDataSO.health;
    
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        _bossHp = FindObjectOfType<BossHp>();

        _leftLaser = transform.Find("LeftLaser").GetComponent<BossLaser>();
        _rightLaser = transform.Find("RightLaser").GetComponent<BossLaser>();
    }
    private void Start()
    {
        _leftLaser.gameObject.SetActive(false);
        _rightLaser.gameObject.SetActive(false);
        _targetTrm = GameManager.Instance.playerController.transform;
    }
    private void Update()
    {
        ChoiseAttackState();
    }

    private void ChangeFace()
    {
        Vector2 vec = _targetTrm.position - transform.position;
        if(vec.x>0)
        {
            _isFaceRight = true;
            _spriteRenderer.flipX = true;
        }
        else
        {
            _isFaceRight = false;
            _spriteRenderer.flipX = false;
        }
    }


    private void ChoiseAttackState()
    {
        if (_isAttack) return;
        _isAttack = true;

        Vector2 dir = _targetTrm.position - transform.position;

        if(_isTwoPhase&& _isTwoPhaseStar==false)
        {
            _isTwoPhaseStar = true;
            MoveTwoPhaseStart();
            return;
        }
        if (dir.sqrMagnitude < 75 && _beforeState != BossState.Attack)
        {
            MeleeAttack();
        }
        else if (_beforeState != BossState.Range)
        {
            RangeAttack();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveDown()
    {
        _currentState = BossState.MoveDown;
        _animator.SetTrigger(_moveDownHashStr);
        StartCoroutine(MoveDownCoroutine());
    }
    private IEnumerator MoveDownCoroutine()
    {
        yield return new WaitForSeconds(4f);
        transform.position = new Vector2(Mathf.Clamp(_targetTrm.position.x, -7, 7), Mathf.Clamp(_targetTrm.position.y, -6, 6));
        _currentState = BossState.MoveUp;
        _animator.SetTrigger(_moveUpHashStr);
        StartCoroutine(WaitForAttackTime());
    }
    private void MoveTwoPhaseStart()
    {
        _currentState = BossState.MoveDown;
        _animator.SetTrigger(_moveDownHashStr);
        StartCoroutine(MoveTwoPhaseCoroutine());
    }
    private IEnumerator MoveTwoPhaseCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;
        transform.position = Vector2.zero;
        _animator.SetTrigger(_moveUpHashStr);
        yield return wait;

        _leftLaser.gameObject.SetActive(true);
        _rightLaser.gameObject.SetActive(true);

        StartCoroutine(RangeAPattern());

        yield return new WaitForSeconds(10f);

        _leftLaser.gameObject.SetActive(false);
        _rightLaser.gameObject.SetActive(false);
    }

    public void CheckMeleeAttack()
    {
        Vector2 vec = _targetTrm.position - transform.position;
        Debug.Log(vec.sqrMagnitude);
        SoundManager.Instance.MeleeSound();
        if((vec.x>0&&_isFaceRight)|| (vec.x < 0 && _isFaceRight == false))
        {
            if(vec.sqrMagnitude<90)
            {
                IHittable hittable = GameManager.Instance.playerController.GetComponent<IHittable>();
                hittable.GetHit(4, gameObject);
            }
        }
    }
    public void CheckMoveAttack()
    {
        Vector2 vec = _targetTrm.position - transform.position;
        Debug.Log(vec);
        Debug.Log(vec.sqrMagnitude);
        if (vec.sqrMagnitude < 65 && vec.y<6&&vec.y>-4f)
        {
            IHittable hittable = GameManager.Instance.playerController.GetComponent<IHittable>();
            hittable.GetHit(4, gameObject);
        }
    }
   
    private void MeleeAttack()
    {
        _currentState = BossState.Attack;
        ChangeFace();
        _animator.SetTrigger(_attackHashStr);
        StartCoroutine(WaitForAttackTime());

    }
    private void RangeAttack()
    {
        _currentState = BossState.Range;
        _animator.SetBool(_rangeAttackHashStr, true);
        _animator.SetTrigger(_attackHashStr);
        int ran = Random.Range(0, 3);
        switch (ran)
        {
            case 0:
                StartCoroutine(RangeAPattern());
                break;
            case 1:
                StartCoroutine(RangeBPattern());
                break;
            case 2:
                StartCoroutine(RangeCPattern());
                break;
            default:
                break;
        }

    }
    

    private IEnumerator RangeAPattern()
    {
        yield return new WaitForSeconds(2f);
        int a = 0;
        for(int i = 0;i<15;i++)
        {
            CircleFire(25, a, transform, 10);
            a+=15;
            yield return new WaitForSeconds(0.5f);
        }
        _animator.SetBool(_rangeAttackHashStr, false);
        StartCoroutine(WaitForAttackTime());
    }
    private IEnumerator RangeBPattern()
    {
        yield return new WaitForSeconds(2f);
        GameObject go;
        BossBullet bullet;
        float ShootingCnt = 60;
        for (int i = 0;i< ShootingCnt; i++)
        {
            go = Bullet();
            go.transform.position = transform.position;
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / ShootingCnt), Mathf.Sin(Mathf.PI* 2 * i / ShootingCnt));
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(dir, Vector2.zero, 0);
            go.SetActive(true);
            go = Bullet();
            go.transform.position = transform.position;
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(-dir, Vector2.zero, 0);
            go.SetActive(true);
            go = Bullet();
            go.transform.position = transform.position;
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(new Vector2(dir.y,-dir.x), Vector2.zero, 0);
            go.SetActive(true);
            go = Bullet();
            go.transform.position = transform.position;
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(new Vector2(-dir.y, dir.x), Vector2.zero, 0);
            go.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        _animator.SetBool(_rangeAttackHashStr, false);
        StartCoroutine(WaitForAttackTime());
    }
    private IEnumerator RangeCPattern()
    {
        
        GameObject go;
        BossBullet bullet;
        int shootingCnt = 10;
        for (int j = 0;j<12;j++)
        {
            Vector2 vec = _targetTrm.position - transform.position;
            for (int i = 0; i < shootingCnt; i++)
            {
                go = Bullet();
                Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / shootingCnt), Mathf.Sin(Mathf.PI * 2 * i / shootingCnt));
                go.transform.position = new Vector2(dir.x, dir.y) + (Vector2)transform.position;
                bullet = go.GetComponent<BossBullet>();
                bullet.SetBullet(vec.normalized, Vector2.zero, 0);
                go.SetActive(true);
            }
            yield return new WaitForSeconds(0.3f);
        }
        _animator.SetBool(_rangeAttackHashStr, false);
        StartCoroutine(WaitForAttackTime());
    }

    private IEnumerator WaitForAttackTime()
    {
        _beforeState = _currentState;
        _currentState = BossState.Idle;
        yield return new WaitForSeconds(2.5f);
        _isAttack = false;
    }
    private void CircleFire(float ShootingCnt, int one, Transform bulletPos, float addspeed)
    {
        GameObject go;
        BossBullet bullet;
        for (int i = 0;i<ShootingCnt;i++)
        {
            go = Bullet();
            go.transform.position = bulletPos.position;
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / ShootingCnt + one) , Mathf.Sin(Mathf.PI * 2 * i / ShootingCnt + one));
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(dir, Vector2.zero, 0);
            go.SetActive(true);

        }
    }
    private GameObject Bullet()
    {
        GameObject go = PoolManager.Instance.GetPooledObject((int)PooledIndex.BossBullet);
        return go;
    }
}
