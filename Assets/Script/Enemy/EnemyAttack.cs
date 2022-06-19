using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAiBrain _enemyBrain;

    [field: SerializeField]
    public float attackDelay { get; set; } = 1;

    protected bool _waitBeforeNextAttack;

    public UnityEvent AttackFeedback;

    private void Awake()
    {
        _enemyBrain = GetComponent<EnemyAiBrain>();
        AwakeChild();
    }

    public virtual void AwakeChild()
    {

    }

    public abstract void Attack(int damage);
    
    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeforeNextAttack = false;
    }

    protected GameObject GetTarget()
    {
        return _enemyBrain.target;
    }
}
