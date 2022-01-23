using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DialogueManager : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private GameObject nextText;
    [field: SerializeField]
    public CanvasGroup dialogueGroup { get; private set; }
    [SerializeField]
    private Queue<string> sentences;

    private string currentSentence;

    private bool isTyping = false;

    private void Awake()
    {

        sentences = new Queue<string>();
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            isTyping = true;
            nextText.SetActive(false);
            dialogueText.DOText(currentSentence, 3f);
        }
        else
        {
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        // dialogue == currentSentence 대사 한줄 끝.
        if (dialogueText.text.Equals(currentSentence))
        {
            Debug.Log("K11");
            nextText.SetActive(true);
            isTyping = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isTyping)
            NextSentence();
    }
}
