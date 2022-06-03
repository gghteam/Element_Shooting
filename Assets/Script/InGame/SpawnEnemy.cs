using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private int count;

    public void SpawnGreenSlime(Transform pos)
    {
        StartCoroutine(Spawn(10,pos.position,30,(int)PooledIndex.GreenSlime));
    }
    public void SpawnRedSlime(Transform pos)
    {
        StartCoroutine(Spawn(20,pos.position,50,(int)PooledIndex.RedSlime));
    }
    public void SpawnBlueSlime(Transform pos)
    {
        StartCoroutine(Spawn(20,pos.position,50,(int)PooledIndex.BlueSlime));
    }
    private IEnumerator Spawn(int c, Vector2 p, int duration,int index)
    {
        int temp = c;

        while(temp > 0)
        {
            yield return null;
            if (GameManager.Instance.IsStopEvent) continue;
            float x = Random.Range(-3, 3) + p.x;
            float y = Random.Range(-3, 3) + p.y;
            GameObject particle = PoolManager.Instance.GetPooledObject(6);
            particle.transform.position = new Vector2(x, y);
            particle.SetActive(true);
            GameObject enemy = PoolManager.Instance.GetPooledObject(index);
            enemy.transform.position = new Vector2(x, y);
            enemy.SetActive(true);
            temp--;
            yield return new WaitForSeconds(duration/c);
        }
    }
}
