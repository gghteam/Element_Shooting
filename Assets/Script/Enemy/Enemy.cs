using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using static DefineCS;
public class Enemy : PoolableMono,IAgent,IHittable,IKnockBack,IStun
{
    [SerializeField]
    private EnemyDataSO _enemyData;
    
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public EnemyAttack enemyAttack { get; set; }
    [SerializeField]
    private EnemyHpbar _stateBar;
    public Action OnDieAnimaiton;
    [SerializeField]
    private Conditions condition;
    public Conditions getCondition
    {
        get
        {
            return condition;
        }
    }
    public bool _isDead = false;
    public bool isElement = false;
    private bool _isStun = false;
    private AgentMovement _agentMovement;


    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }
    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnReset { get; set; }
    public Vector3 _hitPoint { get; private set; }

    public Action OnAttackAnimation;

    private EnemyAiBrain _enemyBrain;

    private void Awake()
    {
        if(enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }
        _enemyBrain =GetComponent<EnemyAiBrain>();
        _enemyBrain.OnFireButtonPress += PerformAttack;
        _agentMovement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        Health = _enemyData.maxHealth;
    }
    
    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        Vector3 bloodDir = (GameManager.Instance.playerController.transform.position -transform.position).normalized;
        Debug.Log(transform.position);
        GameManager.Instance.BloodParticleSystemHandler.SpawnBlood(new Vector3(transform.position.x * 3,transform.position.y,transform.position.z), -bloodDir);

        _stateBar.SetBar(damage);

        Health -= damage;
        if(Health>_enemyData.maxHealth)
        Health = _enemyData.maxHealth;
        //_hitPoint = damageDealer.transform.position;

        OnGetHit?.Invoke();
        DamagePopup.Create(transform.position, -damage, false);
        //Debug.Log("Damaged +"+gameObject+" : "+Health);
        if (Health <= 0)
        {
            _isDead = true;
            OnDie?.Invoke();
        }
    }

    public void Die()
    {
        OnDieAnimaiton?.Invoke();
        StartCoroutine(DieAimation());
    }
    public IEnumerator DieAimation()
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.Instance.Despawn(gameObject);
    }

    public void KnockBack(Vector2 dir,float power,float duration)
    {
        _agentMovement.KnockBack(dir,power,duration);
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroution(duration));
    }
    public IEnumerator StunCoroution(float duration)
    {
        _isStun = true;
        yield return new WaitForSeconds(duration);
        _isStun = false;
    }

    public void PerformAttack()
    {
        if (GameManager.Instance.shield.isAni) return;
        if(_isStun)return;
        if (!_isDead)
        {
            OnAttackAnimation?.Invoke();
            enemyAttack.Attack(_enemyData.damage);
        }
    }

    public override void Reset()
    {
        OnReset?.Invoke();
        Health = _enemyData.maxHealth;
        _isDead = false;
    }
}
