using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRs : MonoBehaviour
{
    [SerializeField]
    private StorySentences storySentences;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("asd");
            storySentences.Read();
        }
    }
}
