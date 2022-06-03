using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

[CreateAssetMenu(menuName = "SO/Maps/MapData")]
public class MapDataSO : ScriptableObject
{
    public int itemCount = 0;
    [Header("몬스터 방 랜덤 개수")]
    public int minMonsterCount = 0;
    public int maxMonsterCount = 0;
    [Header("함정 방 랜덤 개수")]
    public int minTrapCount = 0;
    public int maxTrapCount = 0;
    [Header("가로 랜덤 크기")]
    public int minWidth = 0;
    public int maxWidth = 0;
    [Header("세로 랜덤 크기")]
    public int minHeight = 0;
    public int maxHeight = 0;

    public List<PooledIndex> enemys;
}
