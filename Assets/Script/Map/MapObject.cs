using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    private bool isdown;
    public bool IsDown
    {
        get
        {
            return isdown;
        }
        set
        {
            isdown = value;
        }
    }
    private float speed = 200f;

    private void Update()
    {
        if(isdown)
            transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
