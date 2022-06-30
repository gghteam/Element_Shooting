using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireContain : MonoBehaviour
{
    [SerializeField]
    private List<BoxCollider2D> fireCol;

    public void offFireCol()
    {
        foreach(BoxCollider2D Col in fireCol)
        {
            Col.enabled = false;
        }
    }
}
