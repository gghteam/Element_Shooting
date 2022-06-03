using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class MapSpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemySpawns;

    public void SpawnEnemy(List<EnemyIndex> enemyList)
    {
        foreach(GameObject go in _enemySpawns)
        {
            int index = Random.Range(0, enemyList.Count);

            GameObject enemy;
            enemy = PoolManager.Instance.GetPooledObject((int)enemyList[index]);
            enemy.transform.position = go.transform.position;
            enemy.SetActive(true);
        }
    }
}
