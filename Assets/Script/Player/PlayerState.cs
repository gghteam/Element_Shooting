using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Character _characterState;
    private void Start()
    {
        GameManager.Instance.PlayerATK = _characterState.atk;
    }

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
            GameManager.Instance.PlayerATK = _characterState.atk;
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
