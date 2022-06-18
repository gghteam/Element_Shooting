using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapBomb : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer magic;
    [SerializeField]
    private ParticleSystem bombParticle;

    [SerializeField]
    float duration = 2; 
    [SerializeField]
    float smoothness = 0.02f;

    bool isBomb = false;
    bool isDamage = false;

    IHittable hittable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!isBomb)
            {
                magic.DOFade(1, 1.5f).OnComplete(() =>
                {
                    StartCoroutine(LerpColor(Color.red, true));
                    isBomb = true;
                    isDamage = true;
                    hittable = collision.GetComponent<IHittable>();
                });
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isDamage = false;
        }
    }


    IEnumerator LerpColor(Color color, bool isCheck)
    {
        float progress = 0;
        float increment = smoothness / duration;
        Color OriColor = magic.color;
        while (progress < 1)
        {
            magic.color = Color.Lerp(OriColor, color, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        isBomb = isCheck;
        if (isBomb)
        {
            bombParticle.Play();
            if(isDamage)
            {
                hittable.GetHit(50, gameObject);
            }
            StartCoroutine(LerpColor(Color.white, false));
        }
        magic.DOFade(0, 1f);
    }
}
