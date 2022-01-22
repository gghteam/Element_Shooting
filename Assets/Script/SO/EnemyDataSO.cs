using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public PoolableMono poolPrefab;
    [field: SerializeField]
    public int maxHealth { get; set; } = 3;
    [field: SerializeField]
    public int damage { get; set; } = 1;
}
