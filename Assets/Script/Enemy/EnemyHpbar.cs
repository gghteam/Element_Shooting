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
        slider.value = 1f;
        canvasGroup.alpha = 0;
    }
    public override void SetMaxBar()
    {
        
    }
    public override void SetBar(float gauge)
    {   
        canvasGroup.alpha = 1f;
        maxGaugeValue = maxGaugeValue != 0 ? maxGaugeValue : enemy.Health;
        gauge = gauge/maxGaugeValue;
        slider.value -= gauge;
    }

}
