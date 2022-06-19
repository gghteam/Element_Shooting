using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public GameObject prefab;
    [Range(1, 10)] public int damage = 1;
    [Range(1, 100f)] public float bulletSpeed = 1;

    [Range(0, 5f)] public float explosionRadius = 3f; 

    public Sprite sprite;
    public RuntimeAnimatorController animatorController;

    public float friction = 0f;


    public bool bounce = false; 
    public bool goThroughHit = false; 
    public bool isRayCast = false; 
    public bool isCharging = false; 

    public GameObject impactObstaclePrefab; 
    public GameObject impactEnemyPrefab; 

    [Range(1, 20)] public float knockBackPower = 5f;
    [Range(0.01f, 1f)] public float knockBackDelay = 0.1f;

    public Material bulletMat;

    public float lifeTime = 2f; 

}
