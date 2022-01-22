using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject gameObject;

private void Start() {
    Debug.Log("Gemegemgmemgemgmemgemgme");
}
    private void Update()
    {
        if(gameObject != null)
        ChangeUI();
    }
    public void ChangeUI()
    { 
        text.text = string.Format("Test HP:{0}", gameObject.GetComponent<EnemyContoller>().currentHp);
    }
}
