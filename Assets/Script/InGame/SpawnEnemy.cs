using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private Vector2 pos;

    private void Start()
    {
        StartCoroutine(Spawn(count,pos,5));
    }
    IEnumerator Spawn(int c, Vector2 p, int duration)
    {
        int temp = c;

        while(temp > 0)
        {
            float x = Random.Range(-5, 5) + p.x;
            float y = Random.Range(-5, 5) + p.y;
            GameObject enemy = PoolManager.Instance.GetPooledObject(1);
            enemy.transform.position = new Vector2(x, y);
            enemy.SetActive(true);
            temp--;
            yield return new WaitForSeconds(duration/c);
        }
    }
}
