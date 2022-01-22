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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            panel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetKeyUp(KeyCode.Q)) {
            panel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ToInGame(){
        
        SceneManager.LoadScene("InGame");
    }

    public void SelectElement()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        if (ButtonName == "Fire") GameManager.Instance.playerController.ChangeCondition(Conditions.Fire);
        else if (ButtonName == "Water") GameManager.Instance.playerController.ChangeCondition(Conditions.Water);
        else if (ButtonName == "Wind") GameManager.Instance.playerController.ChangeCondition(Conditions.Wind);
        else if (ButtonName == "Stone") GameManager.Instance.playerController.ChangeCondition(Conditions.Stone);
        panel.gameObject.SetActive(false);
    }
}
