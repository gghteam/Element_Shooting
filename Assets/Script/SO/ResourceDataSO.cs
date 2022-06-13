using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefineCS;

[System.Serializable]
[CreateAssetMenu(menuName ="SO/Item/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    public float rate; //아이템 드랍 확률
    public GameObject itemPrefab;

    [field:SerializeField]
    public ResourceTypeEnum ResourceType { get; set; }
    [SerializeField]
    private int minAmount = 1, maxAmount =  3;

    public AudioClip useSound;

    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount + 1);
    }

    public Color popUpColor;
}
