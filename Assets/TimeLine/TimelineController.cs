using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private SentenceSO _sentenceData;
    private PlayableDirector _playableDirector;
    private IntroDialog _dialog;



    private int _sentencePage = 0;
    private int _lastPageNum;
    private void Awake()
    {
        _lastPageNum = _sentenceData.dialogList.Count;
        _dialog = FindObjectOfType<IntroDialog>();
        _playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {
        _dialog.gameObject.SetActive(false);
    }

    public  void StartDialog()
    {
        if (_sentencePage >= _lastPageNum) return;
        _dialog.gameObject.SetActive(true);
        _dialog.SetDialog(_sentenceData.dialogList[_sentencePage].sentence, _sentenceData.dialogList[_sentencePage].nameStr, _sentenceData.dialogList[_sentencePage].faceNum);
        _sentencePage++;
        _playableDirector.Pause();
        
    }
    public void StartTimeline()
    {
        _playableDirector.Play();
        _dialog.gameObject.SetActive(false);
    }

    public void EndCutScene()
    {
        SceneManager.LoadScene("InGame");
    }
}
