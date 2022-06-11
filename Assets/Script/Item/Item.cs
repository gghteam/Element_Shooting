using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemDataSO _itemDataSO;
    public ItemDataSO ItemData
    {
        get => _itemDataSO;
    }

    private SpriteRenderer _spriteRenderer;

    private ItemTooltip _itemTooltip = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        _spriteRenderer.sprite = ItemData.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ItemInventory inventory = collision.GetComponent<ItemInventory>();
            if(inventory==null)
            {
                //Debug.LogError("Don't have Player ItemInventory");
            }
            inventory.CurrentItem = this;
            OpenItemTooltip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemInventory inventory = collision.GetComponent<ItemInventory>();
            if (inventory == null)
            {
                Debug.LogError("Don't have Player ItemInventory");
            }
            inventory.CurrentItem = null;
            CloseItemTooltip();
        }
    }

    private void OpenItemTooltip()
    {
        _itemTooltip = GameManager.Instance.uiManager.OpenItemTooltip(_itemDataSO, transform.position);
    }

    private void CloseItemTooltip()
    {
        GameManager.Instance.uiManager.CloseWeaponTooltip(_itemTooltip);
    }

}
