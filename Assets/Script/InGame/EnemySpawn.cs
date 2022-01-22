using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyFrefab;
    [SerializeField]
    private float r;
    private float x = 0 ,y = 0;
    private float signedNumber;
    private int stage = 0;
    private float cooltime;
    private int enemyAmount;

    private void Start() {
        PlayerPrefs.SetInt("STAGE",1);
        StartCoroutine(SpawnEnemyTime());
    }
    private IEnumerator SpawnEnemyTime()
    {
        StageInit();
        while(enemyAmount>0)
        {
            SpawnEnemy();
            enemyAmount--;
            yield return new WaitForSeconds(cooltime);
        }
        
    }
    private void SpawnEnemy()
    {
        GameObject enemy = PoolManager.Instance.GetPooledObject(1);
        enemy.transform.position = SpawnPosition();
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
    }
    private Vector2 SpawnPosition()
    {
        signedNumber = Random.Range(-1,1);
        if(signedNumber>=0)
            signedNumber = 1;
        else
            signedNumber = -1;
        x = Random.Range(-r,r);
        y = (Mathf.Sqrt(Mathf.Pow(r,2) - Mathf.Pow(x,2)))*signedNumber;
        return new Vector2(x,y);
    }
    private void StageInit()
    {
        stage = PlayerPrefs.GetInt("STAGE");
        //Debug.Log("The current stage "+stage);
        switch(stage)
        {
            case 1:
            StageOption(0.1f,100);
            return;
            case 2:
            StageOption(1,100);
            return;
        }
    }
    private void StageOption(float cooltime,int enemyAmount)
    {
        this.cooltime = cooltime;
        this.enemyAmount = enemyAmount;
    }
}
