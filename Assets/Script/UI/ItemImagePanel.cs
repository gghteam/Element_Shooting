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
        _itemImage.sprite = sprite;
    }
}
