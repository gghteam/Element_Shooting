using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySentences : MonoBehaviour
{
    [SerializeField]
    private string[] sentences;
    [SerializeField]
    private string[] naming;

    public void Read()
    {
        if (GameManager.Instance.dialogueManager.dialogueGroup.alpha == 0)
        {
            GameManager.Instance.dialogueManager.Ondialogue(sentences, naming);
        }
    }
}
