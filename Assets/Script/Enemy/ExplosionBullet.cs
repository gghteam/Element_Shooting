using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class ExplosionBullet : RegularBullet
{
    protected Collider2D _collider;

    private bool _charging = false; //차징형 인가 아닌가

    public override BulletDataSO BulletData
    {
        get => _bulletData;
        set
        {
            base.BulletData = _bulletData;

            if (_collider == null)
            {
                _collider = GetComponent<Collider2D>();
            }
            _charging = _bulletData.isCharging;
            _collider.enabled = !_charging; //차징이 아닌 총알은 바로 활성화
        }
    }

    public void StartFire()
    {
        _collider.enabled = true;
        _charging = false;
    }

    protected override void FixedUpdate()
    {
        if (_charging) return;

        base.FixedUpdate();
    }

    public override void Reset()
    {
        base.Reset();
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;

        IHittable hittable = other.GetComponent<IHittable>();

        if (hittable != null && hittable.IsEnemy == IsEnemy)
        {
            return; //피아 식별
        }

        _isDead = true;
        PoolManager.Instance.Despawn(this.gameObject);

        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        ImpactScript impact = PoolManager.Instance.GetPooledObject((int)PooledIndex.ExplosionImpact).GetComponent<ImpactScript>();
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 359f)));
        impact.SetPositionAndRotation(transform.position + (Vector3)randomOffset, rot);


        LayerMask enemyLayer = 1 << _enemyLayer;
        Collider2D[] enemyArr = Physics2D.OverlapCircleAll(transform.position, _bulletData.explosionRadius, enemyLayer);
        foreach (Collider2D e in enemyArr)
        {
            IHittable hit = e.GetComponent<IHittable>();
            hit?.GetHit(_bulletData.damage * damageFactor, gameObject);

            IKnockBack ikb = e.GetComponent<IKnockBack>();
            Vector3 kbDir = (e.transform.position - transform.position).normalized;
            ikb?.KnockBack(kbDir, _bulletData.knockBackPower, _bulletData.knockBackDelay);
        }
    }
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject && _bulletData != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _bulletData.explosionRadius);
            Gizmos.color = Color.white;
        }
    }

#endif
}
