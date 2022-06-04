using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Character _characterState;
    
    public Character CharacterState
    {
        get => _characterState;
        set
        {
            _characterState.maxHp += value.maxHp;
            GameManager.Instance.ChangeMaxHealthValue(_characterState.maxHp);
            _characterState.speed += value.speed;
            _characterState.maxStamina += value.maxStamina;
            GameManager.Instance.ChangeMaxStaminaValue(_characterState.maxStamina);
            _characterState.mana += value.mana;

            _characterState.atk += value.atk;
            _characterState.rpm += value.rpm;
            _characterState.mul += value.mul;
        }
    }

    private void Awake()
    {
        GameManager.Instance.ChangeMaxHealthValue(_characterState.maxHp);
        GameManager.Instance.ChangeMaxStaminaValue(_characterState.maxStamina);
    }
}
