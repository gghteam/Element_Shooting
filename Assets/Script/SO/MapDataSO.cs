using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

[CreateAssetMenu(menuName = "SO/Maps/MapData")]
public class MapDataSO : ScriptableObject
{
    public int itemCount = 0;
    [Header("���� �� ���� ����")]
    public int minMonsterCount = 0;
    public int maxMonsterCount = 0;
    [Header("���� �� ���� ����")]
    public int minTrapCount = 0;
    public int maxTrapCount = 0;
    [Header("���� ���� ũ��")]
    public int minWidth = 0;
    public int maxWidth = 0;
    [Header("���� ���� ũ��")]
    public int minHeight = 0;
    public int maxHeight = 0;

    public List<PooledIndex> enemys;
}
