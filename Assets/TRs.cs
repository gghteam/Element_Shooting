using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRs : MonoBehaviour
{
    [SerializeField]
    private int myKey;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("asd");
            GameManager.Instance.storyData.Read(myKey);
        }
    }
}
