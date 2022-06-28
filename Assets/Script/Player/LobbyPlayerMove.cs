using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rigidbody = null;

    private bool isPanel = false;
    private bool isTeleportation = false;

    [SerializeField]
    private Image fadePanel;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Transform[] spawn;

    int index;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isPanel && !isTeleportation)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector2 moveDir = new Vector2(h, v);

            if(moveDir.sqrMagnitude > 0)
            {
                animator.SetFloat("X", h);
                animator.SetFloat("Y", v);
                animator.SetBool("IsMove", true);
            }
            else
                animator.SetBool("IsMove", false);

            moveDir.Normalize();

            rigidbody.velocity = moveDir * speed;
        }
        else
        {
            animator.SetBool("IsMove", false);
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");
        if (collision.CompareTag("GOINGAME"))
        {
            isPanel = true;
            panel.SetActive(isPanel);
        }

        if(collision.CompareTag("Room"))
        {
            index = collision.transform.GetComponent<RoomIndex>().GetIndex();
            isTeleportation = true;
            
            StartCoroutine(PlayFadeOut());

        }
    }

    IEnumerator PlayFadeIn()
    {
        float time = 0f;
        fadePanel.color = new Color(0, 0, 0, 1);

        while (fadePanel.color.a > 0f)
        {
            time += Time.deltaTime / 2;

            // 알파 값 계산.  
            fadePanel.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, time));
            yield return null;
        }
        isTeleportation = false;
    }

    IEnumerator PlayFadeOut()
    {

        float time = 0f;
        fadePanel.color = new Color(0, 0, 0, 0);

        while (fadePanel.color.a < 1f)
        {
            time += Time.deltaTime / 2;

            // 알파 값 계산.  
            fadePanel.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, time));
            yield return null;
        }

        transform.localPosition = spawn[index].position;
        StartCoroutine(PlayFadeIn());
    }

    public void OKButton()
    {
        panel.SetActive(false);
        LoadingSceneController loadingSceneController = FindObjectOfType<LoadingSceneController>();

        if (loadingSceneController == null)
        {
            loadingSceneController = Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
        }

        loadingSceneController.LoadScene("InGame");
    }

    public void NoButton()
    {
        isPanel = false;
        panel.SetActive(isPanel);
    }
}
