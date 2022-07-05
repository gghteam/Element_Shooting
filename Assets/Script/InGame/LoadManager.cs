using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    private int introPlayerPrefab;
    private void Awake()
    {
        //Debug.Log("AAAA");
        //PlayerPrefs.SetInt("Intro", 0);
        //PlayerPrefs.SetInt("TURORIAL", 1);
        introPlayerPrefab = PlayerPrefs.GetInt("Intro");
    }
    public void StartGame()
    {
        if(introPlayerPrefab!=0)
        {
            _canvas.sortingOrder = -1;
            FindObjectOfType<LoadingSceneController>().LoadScene("Lobby");
        }
        else
        {
            _canvas.sortingOrder = -1;
            FindObjectOfType<LoadingSceneController>().LoadScene("Intro");
            PlayerPrefs.SetInt("Intro",1);
        }
        
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
