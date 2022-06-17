using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DefineCS;

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

    private BossState _currentState = BossState.Idle;
    private BossState _deforeState = BossState.Idle;

    private Transform _targetTrm = null;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audio;

    private readonly int _attackHashStr = Animator.StringToHash("Attack");
    private readonly int _rangeAttackHashStr = Animator.StringToHash("RangeAttack");
    private readonly int _moveDownHashStr = Animator.StringToHash("MoveDown");
    private readonly int _moveUpHashStr = Animator.StringToHash("MoveUp");

    private bool _isAttack;
    private bool _isRange;

    #region Inferface
    public Vector3 _hitPoint { get; set; }

    public int Health { get; set; }

    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Debug.Log("Boss Die");
        }
    }
    #endregion


    private void Awake()
    {
        Health = _bossDataSO.health;
    
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _targetTrm = GameManager.Instance.playerController.transform;
    }
    private void Update()
    {
        //State();
        ChoiseAttackState();
    }

    private void State()
    {
        switch(_currentState)
        {
            case BossState.Idle:
                break;
            case BossState.Attack:
                break;
            case BossState.RangeOn:
                break;
            case BossState.Range:
                break;
            case BossState.RangeOff:
                break;
            case BossState.MoveDown:
                break;
            case BossState.MoveUp:
                break;
            case BossState.Death:
                break;

        }
    }

    private void ChoiseAttackState()
    {
        if (_isAttack) return;
        _isAttack = true;
        Vector2 dir = _targetTrm.position - transform.position;
        if(dir.sqrMagnitude < 75)
        {
            MeleeAttack();
        }
        else
        {
            RangeAttack();
        }
    }

    private void MeleeAttack()
    {
        _animator.SetTrigger(_attackHashStr);
        StartCoroutine(WaitForAttackTime());

    }
    private void RangeAttack()
    {
        _animator.SetBool(_rangeAttackHashStr, true);
        _animator.SetTrigger(_attackHashStr);
        StartCoroutine(RangeAPattern());
    }
    private IEnumerator WaitForAttackTime()
    {
        yield return new WaitForSeconds(5f);
        _isAttack = false;
    }

    private IEnumerator RangeAPattern()
    {
        CircleFire(30, 5, transform, 10);
        yield return new WaitForSeconds(1f);

        StartCoroutine(WaitForAttackTime());
    }

    private void CircleFire(float shotingCnt, int one, Transform bulletPos, float addspeed)
    {
        GameObject go;
        BossBullet bullet;
        for (int i = 0;i<shotingCnt;i++)
        {
            go = Bullet();
            go.transform.position = bulletPos.position;
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / shotingCnt), Mathf.Sin(Mathf.PI * 2 * i / shotingCnt));
            Vector3 rot = new Vector3(0f, 0f, 360 * i / shotingCnt - 90);
            bullet = go.GetComponent<BossBullet>();
            bullet.SetBullet(dir, rot, 0);
            go.SetActive(true);

        }
    }
    private GameObject Bullet()
    {
        GameObject go = PoolManager.Instance.GetPooledObject((int)PooledIndex.BossBullet);
        return go;
    }
}
