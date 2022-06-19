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
    private void Awake()
    {
        
    }
    public void StartGame()
    {
        Debug.Log("¤»¤»¤»");
        _canvas.sortingOrder = -1;
        FindObjectOfType<LoadingSceneController>().LoadScene("Lobby");
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
