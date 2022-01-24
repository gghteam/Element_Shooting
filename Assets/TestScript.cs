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
    [SerializeField]
    private Camera camera;

private void Start() {
    Debug.Log("Gemegemgmemgemgmemgemgme");
}
    private void Update()
    {
        if(gameObject != null)
        ChangeUI();
        /*
        if(Input.GetMouseButtonDown(0))
        {
            bool isCriticalHit = Random.Range(0, 100) < 30;
            DamagePopup.Create(camera.ScreenToWorldPoint(Input.mousePosition), 100,isCriticalHit);
        }
        */
    }
    public void ChangeUI()
    { 
        text.text = string.Format("Test HP:{0}", gameObject.GetComponent<EnemyContoller>().currentHp);
    }
}
