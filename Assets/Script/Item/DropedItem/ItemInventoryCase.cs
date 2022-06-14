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

    #region JSON
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/ItemInventory.txt";

    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            _itemCases = JsonUtility.FromJson<Dictionary<string, ItemCasePanel>>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }
    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (_itemCases == null) return;
        string json = JsonUtility.ToJson(_itemCases, true);
        Debug.Log(json);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    #endregion

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        ItemCaseSetting();
    }

    private void ItemCaseSetting()
    {
        List<ItemCasePanel> list = new List<ItemCasePanel>(_itemCases.Values);
        foreach(ItemCasePanel itemCase in list)
        {
            GameObject panel = null;
            panel = Instantiate(itemCase.gameObject, _itemCasePanelTemple.transform.parent);
            panel.SetActive(true);
        }
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
        SaveToJson();
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
