using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class ItemTooltip : PoolableMono
{
    private static int DefaultSortingOrder = 20;

    

    private TextMeshPro _itemNameText;

    private TextMeshPro _addHealthText;
    private TextMeshPro _speedText;
    private TextMeshPro _staminaText;
    private TextMeshPro _manaText;

    private TextMeshPro _atkText;
    private TextMeshPro _rpmText;
    private TextMeshPro _mulText;

    private SpriteRenderer _panelSprite;

    private List<SpriteRenderer> _childSprite = new List<SpriteRenderer>();
    private List<TextMeshPro> _childText = new List<TextMeshPro>();

    private void Awake()
    {
        

        _itemNameText = transform.Find("ItemNameRow/NameText").GetComponent<TextMeshPro>();
        _childText.Add(_itemNameText);
        _addHealthText = transform.Find("AddHealthRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_addHealthText);
        _speedText = transform.Find("SpeedRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_speedText);
        _staminaText = transform.Find("StaminaRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_staminaText);
        _manaText = transform.Find("ManaRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_manaText);

        _atkText = transform.Find("AtkRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_atkText);
        _rpmText = transform.Find("RpmRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_rpmText);
        _mulText = transform.Find("MulRow/ValueText").GetComponent<TextMeshPro>();
        _childText.Add(_mulText);

        _panelSprite = GetComponent<SpriteRenderer>();

        GetComponentsInChildren<SpriteRenderer>(_childSprite);
        _childSprite.RemoveAt(0);
    }

    public void SetText(ItemDataSO data)
    {
        _itemNameText.SetText(data.itemName.ToString());

        _addHealthText.SetText(data.addHealth.ToString());
        _speedText.SetText(data.speed.ToString());
        _staminaText.SetText(data.stamina.ToString());
        _manaText.SetText(data.mana.ToString());

        _atkText.SetText(data.atk.ToString());
        _rpmText.SetText(data.rpm.ToString());
        _mulText.SetText(data.mul.ToString());
    }

    private void SetSortingOrder(int order)
    {
        _panelSprite.sortingOrder = DefaultSortingOrder + order;
        _childSprite.ForEach(x => x.sortingOrder = DefaultSortingOrder + order + 1);
        _childText.ForEach(x => x.sortingOrder = DefaultSortingOrder + order + 1);
    }

    public void PopupTooltip(Vector3 wordPos, int order)
    {
        transform.localScale = new Vector3(1, 0, 1);
        wordPos.y += 0.5f;
        transform.position = wordPos;
        SetSortingOrder(order);
        gameObject.SetActive(true);
        Open();
    }
    private void Open()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(1.2f,0.3f));
        seq.Append(transform.DOScaleY(0.9f, 0.1f));
        seq.Append(transform.DOScaleY(1f, 0.1f));
    }

    public void CloseTooltip()
    {
        Close();
    }

    private void Close()
    {
        DOTween.Kill(transform);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(1.2f, 0.1f));
        seq.Append(transform.DOScaleY(0f, 0.3f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Despawn(this.gameObject);
        });
    }

    public override void Reset()
    {
        transform.localScale = new Vector3(1, 0, 1);
        SetSortingOrder(0);
    }
}
