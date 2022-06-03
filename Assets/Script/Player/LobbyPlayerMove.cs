using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rigidbody = null;

    private bool isPanel = false;

    [SerializeField]
    private GameObject panel;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isPanel)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector2 moveDir = new Vector2(h, v);

            moveDir.Normalize();

            rigidbody.velocity = moveDir * speed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ãæµ¹");
        if (collision.CompareTag("GOINGAME"))
        {
            isPanel = true;
            panel.SetActive(isPanel);
        }
    }

    public void OKButton()
    {
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
