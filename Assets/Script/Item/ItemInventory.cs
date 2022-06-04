using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private PlayerState _playerState;

    private ItemDataSO _addItemData;

    private  Item _currentItem;
    public Item CurrentItem { private get => _currentItem; set { _currentItem = value; } }

    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
    }
    public void GetActiveItem()
    {
        if (CurrentItem != null)
        {
            _addItemData =  CurrentItem.ItemDataSO;
            SetPlayerStateValue(_addItemData);
            CurrentItem.gameObject.SetActive(false);
            Debug.Log("GetItem!!!");
        }
        else
        {
            Debug.Log("Noting Item");
        }
    }

    private void SetPlayerStateValue(ItemDataSO itemData)
    {
        Character character = new Character();

        character.maxHp = itemData.addHealth;
        character.maxStamina = itemData.stamina;
        character.speed = itemData.speed;
        character.mana = itemData.mana;

        character.atk = itemData.atk;
        character.rpm = itemData.rpm;
        character.mul = itemData.mul;

        _playerState.CharacterState = character;
    }
}
