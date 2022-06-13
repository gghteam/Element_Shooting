using DG.Tweening;
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
        set
        {
            _itemDataSO = value;
            if(_spriteRenderer==null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.sprite = _itemDataSO.itemSprite;
        }
    }

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private ItemTooltip _itemTooltip = null;

    private bool _isActive = false;

    private void Awake()
    {
        if(_spriteRenderer==null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _collider = GetComponent<Collider2D>();
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

    public void SpawnInBox(Vector3 pos,float power,float time)
    {
        _collider.enabled = false;
        transform.DOJump(pos, power, 1, time).OnComplete(() => _collider.enabled = true);
    }
}
