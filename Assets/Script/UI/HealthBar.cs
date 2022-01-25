using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : StateBar
{
    public override void AwakeChild()
    {
        maxGaugeValue = GameManager.Instance.PlayerInfo.maxHp;
        SetMaxBar();
    }
    public override void SetMaxBar()
    {
        slider.value = 1f;

        fill.color = gradient.Evaluate(1f);
    }
    public override void SetBar(float gauge)
    {
        gauge = gauge/maxGaugeValue;
        slider.value = gauge;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
