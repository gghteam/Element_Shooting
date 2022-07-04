using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialog : MonoBehaviour
{
    [SerializeField]
    private Text _sentenceText;
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Image _faceImage;

    [SerializeField]
    private Sprite[] _faceSprites;

    [SerializeField]
    private GameObject _nextBtn;

    private TimelineController _timeline;

    private bool _isTouch = false;

    private void Awake()
    {
        _timeline = FindObjectOfType<TimelineController>();
        _nextBtn.SetActive(false);
    }
    public void SetDialog(string sentence,string name,int faceNum)
    {
        _nameText.text = name;
        _faceImage.sprite = _faceSprites[faceNum];
        StartCoroutine(StartDialog(sentence));
    }
    public void TouchPanel()
    {
        _isTouch = true;
    }
    private IEnumerator StartDialog(string sentence)
    {
        _isTouch = false;
        _sentenceText.text = "";
        foreach(char latter in sentence.ToCharArray())
        {
            _sentenceText.text += latter;
            if(!_isTouch)
            {
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
        _nextBtn.SetActive(true);
    }
}
