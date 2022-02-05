using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer effect;
    [SerializeField]
    private BoxCollider2D box;
    [SerializeField]
    private new Camera camera;
    public GameObject red;
    public GameObject blue;

    private bool isEnter = false;
    private bool isExit = false;
    private bool isend = false;
    private bool isEye = false;
    private bool isS = false;
    private bool isQ = false;
    private Vector3 temp;
    public bool isAni { get; set; } = false;

    private int count = 0;
    private int check = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isend) return;
        isEnter = true;
        isExit = false;
        box.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isend) return;
        isExit = true;
        isEnter = false;
        box.enabled = false;
    }

    private void Update()
    {
        if(isEnter)
        {
            if (isS) return;
            if(effect.color.a < 255)
            {
                Color color = new Color(0, 0, 0, Time.deltaTime);
                effect.color += color;
            }
        }
        if(isExit)
        {
            if (effect.color.a > 0)
            {
                Debug.Log("QIN");
                Color color = new Color(0, 0, 0, Time.deltaTime);
                effect.color -= color * 2;
            }
            else isExit = false;
        }
        if(isS)
        {
            if (effect.color.a > 0)
            {
                Color color = new Color(0, 0, 0, Time.deltaTime);
                effect.color -= color;
            }
            else isS = false;
        }
        if(isAni && !isEye)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, 10.75f, -10f), 0.05f);
        }
        if(camera.transform.position == new Vector3(0, 10.75f, -10f))
        {
            if (isEye) return;
            isEye = true;
            if(check == 0)
            StartCoroutine(LightEye());

        }

        if(isQ)
        {
            Debug.Log("1234");
            camera.transform.position = Vector3.Lerp(camera.transform.position, temp, 0.05f);
        }
        if(camera.transform.position == temp)
        {
            if(isAni && isEye && isQ)
            {
                isAni = false;
                isEye = false;
                isQ = false;

            }
        }
    }

public void Check()
{
    Debug.Log("Ȯ��");
    count++;
    if (count >= 2)
    {
        temp = camera.transform.position;
        isend = true;
        isAni = true;
        isEnter = false;
        isExit = false;
        effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, 1);
    }
}

    private IEnumerator LightEye()
    {
        check++;
        Debug.Log("Q");
        yield return new WaitForSeconds(2f);
        while(red.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a < 255)
        {
            red.transform.GetChild(0).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            red.transform.GetChild(1).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.005f);
        }
        while (blue.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a < 255)
        {
            blue.transform.GetChild(0).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            blue.transform.GetChild(1).GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.005f);
        }
        isS = true;
        yield return new WaitForSeconds(4f);
        effect.gameObject.SetActive(false);
        isQ = true;
        camera.transform.position = Vector3.Lerp(camera.transform.position, temp, 0.05f);
    }
}
