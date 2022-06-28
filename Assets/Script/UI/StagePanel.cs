using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    [SerializeField]
    private float _showTime = 3f;

    [SerializeField]
    private TextMeshProUGUI _stageMeshText;

    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _stageMeshText.SetText("{0} - Stage", PlayerPrefs.GetInt("CurrentLevel"));
        ShowPanel();
    }



    private void ShowPanel()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_rectTrm.DOScaleY(0, 0.1f));
        seq.Append(_rectTrm.DOScaleY(1,_showTime));
        seq.AppendInterval(2f);
        seq.Append(_rectTrm.DOScaleY(0, _showTime));
    }
}
