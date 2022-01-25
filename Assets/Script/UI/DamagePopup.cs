using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : PoolableMono
{
    //Create a Damage Popup
    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticlaHit)
    {
        float x = Random.Range(-0.5f,0.5f),y= Random.Range(-0.5f,0.5f);
        GameObject damagePopupObject = PoolManager.Instance.GetPooledObject(3);
        damagePopupObject.transform.position = new Vector3(position.x+x,position.y+y,position.z);
        damagePopupObject.transform.position = new Vector3(damagePopupObject.transform.position.x, damagePopupObject.transform.position.y, 0);
        damagePopupObject.transform.rotation = Quaternion.identity;
        DamagePopup damagePopup = damagePopupObject.transform.GetComponent<DamagePopup>();
        if(damageAmount > 0) 
        damagePopup.Setup("+" + damageAmount.ToString(), isCriticlaHit,false);
        else
        damagePopup.Setup(damageAmount.ToString(), isCriticlaHit,true);
        damagePopupObject.SetActive(true);

        return damagePopup;
    }

    private static int sortindOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disapperTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string damageAmount, bool isCriticlaHit, bool isNegative)
    {
        textMesh.SetText(damageAmount);
        if(!isCriticlaHit)
        {
            //Normal hit
            textMesh.fontSize = 5;
            if (!isNegative)
                ColorUtility.TryParseHtmlString("#73FF00", out textColor);
            else
                ColorUtility.TryParseHtmlString("#FFC500", out textColor);
        }
        else
        {
            textMesh.fontSize = 7;
            ColorUtility.TryParseHtmlString("#FF2000", out textColor);
        }
        textMesh.color = textColor;
        disapperTimer = DISAPPEAR_TIMER_MAX;

        sortindOrder++;
        textMesh.sortingOrder = sortindOrder;
        moveVector = new Vector3(0.05f, 0.1f) * 60f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disapperTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            //First half of the popup lifeTime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //Second half of the popup lifeTime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disapperTimer -= Time.deltaTime;
        if(disapperTimer < 0)
        {
            //start disappering
            float disapperSpeed = 3f;
            textColor.a -= disapperSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                PoolManager.Instance.Despawn(gameObject);
            }
        }
    }

    public override void Reset()
    {
        //NULL
    }
}
