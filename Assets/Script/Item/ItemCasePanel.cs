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

    private int _itemCnt = 1;

    [SerializeField]
    private Sprite sprite1;

    public void SetValue(Sprite sprite)
    {
        _itemImage.sprite = sprite;
        _itemCntText.text = string.Format("{0}", _itemCnt);
    }
    public void AdditemCnt()
    {
        _itemCnt++;
    }
}
