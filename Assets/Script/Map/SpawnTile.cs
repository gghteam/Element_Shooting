using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //생성될 타일
    [SerializeField]
    private GameObject tile;

    private void Start()
    {
        GameObject instance = Instantiate(tile, transform.position, Quaternion.identity);
        //생성한 부모를 해당 transform으로..
        instance.transform.parent = transform;
    }
}
