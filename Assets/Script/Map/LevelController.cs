using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

public class LevelController : MonoBehaviour
{
    public int itemCount { get; private set; }
    public int monsterCount { get; private set; }
    public int trapCount { get; private set; }
    public int width { get; private set; }
    public int height { get; private set; }

    public List<EnemyIndex> enemysList = new List<EnemyIndex>();

    [SerializeField]
    private List<MapDataSO> LevelMapDatas = new List<MapDataSO>();

    public int current;

    public void SetLevelMap()
    {
        //PlayerPrefs.SetInt("CurrentLevel", 1);
        current = PlayerPrefs.GetInt("CurrentLevel", 1);
        MapDataSO currentMapData = LevelMapDatas[current - 1];
        itemCount = currentMapData.itemCount;
        monsterCount = Random.Range(currentMapData.minMonsterCount, currentMapData.maxMonsterCount + 1);
        trapCount = Random.Range(currentMapData.minTrapCount, currentMapData.maxTrapCount + 1);
        width = Random.Range(currentMapData.minWidth, currentMapData.maxWidth);
        height = Random.Range(currentMapData.minHeight, currentMapData.maxHeight);
        enemysList = currentMapData.enemys;
    }
}
