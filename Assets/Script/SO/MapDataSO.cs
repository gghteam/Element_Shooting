using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Maps/MapData")]
public class MapDataSO : ScriptableObject
{
    public int itemCount = 0;
    public int monsterCount = 0;
    public int trapCount = 0;
    public int width = 5;
    public int height = 5;
}
