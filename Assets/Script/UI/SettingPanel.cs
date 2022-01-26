using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public void OnExitBtn()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void OnContinueBtn()
    {
        _isOpen = true;
        Time.timeScale = 1;
        StartCoroutine(OffBookAnimation());
    }
    public void OnRePlayPanel()
    {
        
    }
    private void Update() {
        if(_isOpen)return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!settingPanel.activeSelf)
            {
                
                _isOpen = true;
                settingPanel.SetActive(true);
                StartCoroutine(OpenBookAnimation());
                
            }else
            {
                _isOpen = true;
                Time.timeScale = 1;
                StartCoroutine(OffBookAnimation());
            }
        }
    }
    private IEnumerator OpenBookAnimation()
    {
        float upDownY = 1000;
        bookRectPosition.DOAnchorPosY(bookRectPosition.anchoredPosition.y+upDownY,1f);
        animator.Play("Open");
        yield return new WaitForSeconds(1.2f);
        SetSetting(true,0);
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
