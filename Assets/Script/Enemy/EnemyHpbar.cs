using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : StateBar
{
    [SerializeField]
    private Enemy enemy;
    public override void AwakeChild()
    {
        maxGaugeValue = 0;
        SetMaxBar();
    }
    public override void SetMaxBar()
    {
        slider.value = 1f;
        maxGaugeValue = (float)enemy.Health;
    }
    public override void SetBar(float gauge)
    {   
        maxGaugeValue = maxGaugeValue != 0 ? maxGaugeValue : enemy.Health;
        gauge = gauge/maxGaugeValue;
        slider.value -= gauge;
    }

}
