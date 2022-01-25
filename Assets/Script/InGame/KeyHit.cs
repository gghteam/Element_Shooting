using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHit : MonoBehaviour
{
    [SerializeField]
    private StorySentences storySentences = null;

    private bool isStay = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isStay = true;
        }
    }

    private void Update()
    {        
        if(Input.GetKeyDown(KeyCode.E) && isStay)
        {
            storySentences.Read();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isStay = false;
        }
    }
}
