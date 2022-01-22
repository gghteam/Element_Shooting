using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    private float maxHealth;
    private void Awake() {
        maxHealth = GameManager.Instance.PlayerInfo.maxHp;
        SetMaxHealth();
    }
    public void SetMaxHealth()
    {
        slider.value = 1f;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        health = health/maxHealth;
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
