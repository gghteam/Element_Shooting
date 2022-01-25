using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D box;
    [SerializeField]
    private GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            effect.transform.GetComponent<MeshRenderer>().enabled = true;
            box.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            effect.transform.GetComponent<MeshRenderer>().enabled = false;
            box.enabled = false;
        }
    }
}
