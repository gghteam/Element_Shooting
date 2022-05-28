using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Image panel;
    [SerializeField]
    private Image mark;
    [SerializeField]
    private Image magicColor;
    [SerializeField]
    private GameObject setttingPanel;
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private float magicSpeed;
    [SerializeField]
    private Sprite[] selectMark;
    [SerializeField]
    private Color[] markColor;
    [field : SerializeField]
    public Sprite[] elementMark { get; private set; }
    private void Start() {
        mark.sprite = selectMark[0];
        mark.color = markColor[0];
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.playerController._isSelectElement = true;
            panel.gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Q)) {
            panel.gameObject.SetActive(false);
            GameManager.Instance.playerController._isSelectElement = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
        Rotate();
    }    
    private void Rotate()
    {
        magic.gameObject.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, magicSpeed * Time.deltaTime));
    }
    
    public void SelectElement()
    {
       
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        if (ButtonName == "Fire") {
            mark.sprite = selectMark[0];
            mark.color = markColor[0];
            magicColor.color = markColor[0];
            GameManager.Instance.playerController.ChangeCondition(Conditions.Fire);
        }else if (ButtonName == "Water") {
            mark.sprite = selectMark[1];
            mark.color = markColor[1];
            magicColor.color = markColor[1];
            GameManager.Instance.playerController.ChangeCondition(Conditions.Water);
        }else if (ButtonName == "Wind") {
            mark.sprite = selectMark[2];
            mark.color = markColor[2];
            magicColor.color = markColor[2];
            GameManager.Instance.playerController.ChangeCondition(Conditions.Wind);
        }else if (ButtonName == "Stone") {
            mark.sprite = selectMark[3];
            mark.color = markColor[3];
            magicColor.color = markColor[3];
            GameManager.Instance.playerController.ChangeCondition(Conditions.Stone);
        }
        panel.gameObject.SetActive(false);
    }
}
