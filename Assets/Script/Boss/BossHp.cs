using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    private Slider _slider;

    private float _value = 1;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    private void Update()
    {
        SetActive();
    }

    private void SetActive()
    {
        if(_value <= 0)
        {
            gameObject.SetActive(false);
        }
        _slider.value = _value;
    }

    public void SetValue(float value)
    {
        _value = value;
    }
}
