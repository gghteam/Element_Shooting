using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StateBar : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup canvasGroup;
    protected Slider slider = null;
    [SerializeField]
    protected Image fill;
    [SerializeField]
    protected Image mark;
    [SerializeField]
    protected Gradient gradient;
    protected float maxGaugeValue;
    private void Awake() {
        slider = GetComponent<Slider>();
        AwakeChild();
    }
    public virtual void AwakeChild() {
        //nothing
    }
    public abstract void SetMaxBar();
    public abstract void SetBar(float gauge);
}
