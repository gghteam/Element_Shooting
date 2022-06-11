using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImagePanel : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;


    public void SetValue(Sprite sprite)
    {
        Debug.Log(_itemImage);
        _itemImage.sprite = sprite;
    }
}
