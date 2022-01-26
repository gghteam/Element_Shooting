using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;
    [SerializeField]
    private GameObject book;
    [SerializeField]
    private RectTransform bookRectPosition;
    [SerializeField]
    private Animator animator;
    private bool _isOpen = false;
    private bool _isDead = false;
    public void OnExitBtn()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void OnContinueBtn()
    {
        Time.timeScale = 1;
        StartCoroutine(OffBookAnimation());
    }
    public void OnRePlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("InGame");
    }
    public void OnRePlayPanel()
    {
        _isDead = true;
        StartCoroutine(OnRePlayPanelCorutine());
    }
    
    private void Update() {
        if(_isOpen)return;
        if(_isDead)return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!settingPanel.activeSelf)
            {
                
                _isOpen = true;
                settingPanel.SetActive(true);
                StartCoroutine(OpenBookAnimation(0));
                
            }else
            {
                _isOpen = true;
                Time.timeScale = 1;
                StartCoroutine(OffBookAnimation());
            }
        }
    }
    private IEnumerator OnRePlayPanelCorutine()
    {
        yield return new WaitForSeconds(2f);
        settingPanel.SetActive(true);
        StartCoroutine(OpenBookAnimation(1));

    }
    private IEnumerator OpenBookAnimation(int index)
    {
        float upDownY = 1000;
        bookRectPosition.DOAnchorPosY(bookRectPosition.anchoredPosition.y+upDownY,1f);
        animator.Play("Open");
        yield return new WaitForSeconds(1.2f);
        SetSetting(true,index);
        _isOpen = false;
        Time.timeScale = 0;
    }
    private IEnumerator OffBookAnimation()
    {
        float upDownY = -1000;
        SetSetting(false,0);
        bookRectPosition.DOAnchorPosY(bookRectPosition.anchoredPosition.y+upDownY,1f);
        animator.Play("Off");        
        yield return new WaitForSeconds(1f);
        settingPanel.SetActive(false);
        _isOpen = false;
        
    }
    private void SetSetting(bool _isSet,int index)
    {
        book.transform.GetChild(index).gameObject.SetActive(_isSet);
    }
}
