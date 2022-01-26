using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject water;
    [SerializeField]
    private RectTransform nameText;

    private void Start()
    {
        nameText.DOAnchorPosY(-250,0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        fire.transform.Rotate(0, 0, -Time.deltaTime * 200, Space.Self);
        water.transform.Rotate(0, 0, Time.deltaTime * 200, Space.Self);
        if(Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
