using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemInventoryCase : MonoBehaviour
{
    [SerializeField]
    private ItemCasePanel _itemCasePanelTemple;

    private Dictionary<string, ItemCasePanel> _itemCases = new Dictionary<string, ItemCasePanel>();

    private void Awake()
    {
        //ItemCaseSetting();
    }

    private void ItemCaseSetting()
    {
        
    }

    public void InputItemCase(ResourceDataSO data)
    {

        if(_itemCases.ContainsKey(data.itemName))
        {
            _itemCases[data.itemName].AdditemCnt(data.GetAmount());
        }
        else
        {
            NewCreateItemCase(data);
        }
    }
    private void NewCreateItemCase(ResourceDataSO data)
    {
        GameObject panel = null;
        ItemCasePanel itemCase = null;
        panel = Instantiate(_itemCasePanelTemple.gameObject, _itemCasePanelTemple.transform.parent);
        itemCase = panel.GetComponent<ItemCasePanel>();
        itemCase.SetValue(data.itemSprite, data.GetAmount(),data.itemExplanation);
        panel.SetActive(true);
    }
}
