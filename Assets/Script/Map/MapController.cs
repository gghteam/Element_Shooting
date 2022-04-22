using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialMap;
    private void Awake()
    {
       if(PlayerPrefs.GetInt("TURORIAL",1) == 1)
        {
            GameObject map = Instantiate(tutorialMap, new Vector3(0, 0, 0), Quaternion.identity);
            map.transform.parent = gameObject.transform;
            PlayerPrefs.SetInt("TURORIAL", 0);
        }
       else
        {

        }
    }
}
