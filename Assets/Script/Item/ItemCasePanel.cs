using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCasePanel : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private Text _itemCntText;

    private String _itemExplanation;

    private int _itemCnt = 0;

    [SerializeField]
    private Sprite sprite1;

    public void SetValue(Sprite sprite,int count,string ex)
    {
        _itemImage.sprite = sprite;
        _itemExplanation = ex;
        _itemCnt += count;
        _itemCntText.text = string.Format("{0}", _itemCnt);
    }
    public void AdditemCnt(int count)
    {
        _itemCnt += count;
        UpdateSetValue();
    }

    private void UpdateSetValue()
    {
        _itemCntText.text = string.Format("{0}", _itemCnt);
    }
}
