using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private GameObject nextText;
    [field: SerializeField]
    public CanvasGroup dialogueGroup { get; private set; }
    [SerializeField]
    private Queue<string> sentences;
    [SerializeField]
    private Queue<string> nameSentences;

    private string currentSentence;

    private bool isTyping = false;

    public bool IsDialogue { get; private set; } = false;

    [SerializeField]
    private float typingSpeed = 0.1f;

    private void Awake()
    {

        sentences = new Queue<string>();
        nameSentences = new Queue<string>();
    }

    public void Ondialogue(string[] lines, string[] names)
    {
        sentences.Clear();
        IsDialogue = true;
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        foreach (string namet in names)
        {
            nameSentences.Enqueue(namet);
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
            nameText.text = nameSentences.Dequeue();
            isTyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
            //dialogueText.text = "";
            //dialogueText.DOText(currentSentence, 3f);
        }
        else
        {
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
            IsDialogue = false;
        }
    }

    private IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        //TocharArray:문자열을 char형 배열로 변환
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void Update()
    {
        // dialogue == currentSentence ��� ���� ��.
        if (dialogueText.text.Equals(currentSentence) && isTyping)
        {
            nextText.SetActive(true);
            isTyping = false;
        }
    }

    public void ClickDialogue()
    {
        if (!isTyping)
            NextSentence();
    }
}
