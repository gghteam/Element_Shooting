using System;

public class HealthBar : StateBar
{
    public Action UpdateValue;
    public override void AwakeChild()
    {
        maxGaugeValue = 0;
    }
    private void Start() {
        SetMaxBar();
    }
    public override void SetMaxBar()
    {
        slider.value = 1f;
        fill.color = gradient.Evaluate(1f);
        UpdateValue = new Action(DoUpdateValue);
        EventManager.StartListening(EventManager.EventName.PLAYER_DAMAGED,UpdateValue);

    }
    public override void SetBar(float gauge)
    {
        
    }
    private void DoUpdateValue()
    {
        slider.value = GameManager.Instance.GetHpBar();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventManager.EventName.PLAYER_DAMAGED, UpdateValue);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening(EventManager.EventName.PLAYER_DAMAGED, UpdateValue);
    }
}
