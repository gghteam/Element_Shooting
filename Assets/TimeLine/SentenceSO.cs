using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialog
{
    public string nameStr;
    public string sentence;
    public int faceNum;
}


[CreateAssetMenu(menuName ="SO/Timeline/Sentence")]
public class SentenceSO : ScriptableObject
{
    [field:SerializeField]
    public List<Dialog> dialogList;
}
