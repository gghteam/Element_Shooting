using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Item/ItemBoxData")]
public class ItemBoxDataSO : ScriptableObject
{
    public List<ItemDataSO> itemList;

    [Range(1,5)]
    public int maxDropItem = 1;
}
