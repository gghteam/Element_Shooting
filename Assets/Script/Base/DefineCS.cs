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
    public enum PooledIndex
    {
        Not = 0,
        GreenSlime = 1,
        BlueSlime = 4,
        RedSlime = 5,
        DungenGreenSlime = 7,
        DungenBlueSlime = 8,
        DungenRedSlime = 9,
        Bat = 10,
        ItemTooltip = 11,
        Item = 12,
        MonsterDropItem = 13,
        BossBullet = 14,
        Spider = 15,
        Skeleton = 16,

        ExplosionBullet,
        ExplosionImpact,
        RegularBullet

    }

    public enum ResourceTypeEnum
    {
        None,
        Coin,
        MonsterDropItem,
        MagicSpell
    }
}
