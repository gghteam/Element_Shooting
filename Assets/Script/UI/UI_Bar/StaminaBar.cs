using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaminaBar : StateBar
{
    public Action UpdateValue;
    private void Start() {
        SetMaxBar();
    }
    public override void SetMaxBar()
    {
        slider.value = 1;
        slider.maxValue = 1;
        UpdateValue = new Action(DoUpdateValue);
        EventManager.StartListening(EventManager.EventName.PLAYER_RUN,UpdateValue);
    }
    public override void SetBar(float gauge)
    {
        throw new System.NotImplementedException();
    }
    private void DoUpdateValue()
    {
        slider.value = GameManager.Instance.GetStaminaBar();
    }
}
