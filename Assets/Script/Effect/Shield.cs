using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer effect;
    [SerializeField]
    private BoxCollider2D box;

    private bool isEnter = false;
    private bool isExit = false;
    private bool isend = false;
    private int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isend) return;
        isEnter = true;
        isExit = false;
        box.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isend) return;
        isExit = true;
        isEnter = false;
        box.enabled = false;
    }

    private void Update()
    {
        if(isEnter)
        {
            if(effect.color.a < 255)
            {
                Color color = new Color(0, 0, 0, Time.deltaTime);
                effect.color += color;
            }
        }
        if(isExit)
        {
            if(effect.color.a > 0)
            {
                Color color = new Color(0, 0, 0, Time.deltaTime);
                effect.color -= color * 2;
            }
        }
    }

    public void Check()
    {
        Debug.Log("È®ÀÎ");
        count++;
        if(count >= 2)
        {
            isend = true;
            effect.gameObject.SetActive(false);
        }
    }
}
