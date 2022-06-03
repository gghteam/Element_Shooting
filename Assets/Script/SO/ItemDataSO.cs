using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Item/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;

    public int addHealth;
    public float speed;
    public float stamina;
    public float mana;

    public int atk;
    public int rpm;
    public int mul;
}
