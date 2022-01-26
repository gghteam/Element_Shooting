using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefineCS
{
    public enum Conditions
    {
        Not,
        Fire, //1 Fire -> Water  
        Water, //2
        Wind, //3
        Stone, //4
        Grass 
    }
    public enum EnemyIndex
    {
        Not = 0,
        GreenSlime = 1,
        BlueSlime = 4,
        RedSlime = 5

    }
}
