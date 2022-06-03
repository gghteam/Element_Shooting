using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private ItemDataSO _addItemData;

    private  Item _currentItem;
    public Item CurrentItem { private get => _currentItem; set { _currentItem = value; } }


    public void GetActiveItem()
    {
        if (CurrentItem != null)
        {
            _addItemData =  CurrentItem.ItemDataSO;
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

    }
}
