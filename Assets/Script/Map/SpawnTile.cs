using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //������ Ÿ��
    [SerializeField]
    private GameObject tile;

    private void Start()
    {
        GameObject instance = Instantiate(tile, transform.position, Quaternion.identity);
        //������ �θ� �ش� transform����..
        instance.transform.parent = transform;
    }
}
