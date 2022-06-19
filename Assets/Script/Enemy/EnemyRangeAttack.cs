using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField]
    private BulletDataSO _bulletData;
    [SerializeField]
    private Transform _firePos;


    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            _enemyBrain.SetAttackState(true);
            AttackFeedback?.Invoke();

            Transform target = GetTarget().transform;

            Vector2 aimDirection = target.position - _firePos.position;
            float desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(desireAngle, Vector3.forward);

            SpawnBullet(_firePos.position, rot, true, damage);
            StartCoroutine(WaitBeforeAttackCoroutine());

        }
    }

    private void SpawnBullet(Vector3 pos, Quaternion rot, bool isEnemyBullet, int damage)
    {
        EnemyBullet b = PoolManager.Instance.GetPooledObject((int)PooledIndex.ExplosionBullet).GetComponent<EnemyBullet>();
        b.SetPositionAndRotation(pos, rot);
        b.IsEnemy = isEnemyBullet;
        b.BulletData = _bulletData;
        b.damageFactor = damage;
    }
}
