using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private PlayerState _playerState;

    private ItemDataSO _addItemData;

    private  Item _currentItem;
    public Item CurrentItem { private get => _currentItem; set { _currentItem = value; } }

    private List<ItemData> _loadingItemList;

    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
    }
    private void Start()
    {
        LoadingItems();
    }
    private void LoadingItems()
    {
        if (GameManager.Instance.User._ativeItemList.Count <= 0 ) return;

        _loadingItemList = GameManager.Instance.User._ativeItemList;

        foreach (ItemData item in _loadingItemList)
        {

            ItemDataSO input = ScriptableObject.CreateInstance<ItemDataSO>();
            input.itemName = item.itemName;
            input.itemName = item.itemName;
            input.addHealth = item.addHealth;
            input.speed = item.speed;
            input.stamina = item.stamina;
            input.mana = item.mana;
            input.atk = item.atk;
            input.rpm = item.rpm;
            input.mul = item.mul;

            SetPlayerStateValue(input);
            string loadStr = "Items/" + item.itemName;
            GameManager.Instance.uiManager.AddItemIamge(Resources.Load<Sprite>(loadStr));
        }
    }
    public void GetActiveItem()
    {
        if (CurrentItem != null)
        {
            _addItemData =  CurrentItem.ItemDataSO;
            SetPlayerStateValue(_addItemData);
            
            GameManager.Instance.User._ativeItemList.Add(CurrentItem.Data); //itemData -> Json Save
            GameManager.Instance.SaveToJson();
            GameManager.Instance.uiManager.AddItemIamge(_addItemData.itemSprite);
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
