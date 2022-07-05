using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderContainer : MonoBehaviour
{
    [SerializeField]
    private List<BoxCollider2D> fireCol;

    public void offBoxCol()
    {
        foreach(BoxCollider2D Col in fireCol)
        {
            Col.enabled = false;
        }
    }
}
