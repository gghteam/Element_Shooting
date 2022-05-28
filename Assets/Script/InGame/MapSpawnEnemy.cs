using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemySpawns;

    private void Start()
    {
        foreach(GameObject go in _enemySpawns)
        {
            int index = int.Parse(go.name);


            GameObject enemy;
            enemy = PoolManager.Instance.GetPooledObject(index);
            enemy.transform.position = go.transform.position;
            enemy.SetActive(true);
        }
    }
}
