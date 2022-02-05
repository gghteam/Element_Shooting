using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDeBuff : MonoBehaviour
{
    [SerializeField]
    private FireParme fireParme = new FireParme();
    [SerializeField]
    private WaterParme waterParme = new WaterParme();
    [SerializeField]
    private WindParme windParme = new WindParme();
    [SerializeField]
    private StoneParme stoneParme = new StoneParme();

    private AgentMovement movement;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;
    private void Awake() {
        enemy = GetComponent<Enemy>();
        movement = GetComponent<AgentMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void FireDeBuff()
    {
        if(gameObject.activeSelf==false)return;
        StartCoroutine(FireDeBuffCoroutine());
    }
    public void WaterDeBuff()
    {
        if(gameObject.activeSelf==false)return;
        StartCoroutine(WaterDeBuffCoroutine());
    }
    public void WindDeBuff()
    {
        if(gameObject.activeSelf==false)return;
        StartCoroutine(WindDeBuffCoroutine());
    }
    public void StoneDeBuff()
    {
        if(gameObject.activeSelf==false)return;
        StartCoroutine(StoneDeBuffCoroutine());
    }

    private IEnumerator FireDeBuffCoroutine()
    {
        IHittable hittable = enemy.GetComponent<IHittable>();
        if (enemy.isElement) yield break;
        enemy.isElement = true;
        float time = fireParme.Maxduration;
        StartCoroutine(OnElementDamagedAnimation(spriteRenderer, new Color(1, 0, 0, 1), fireParme.duration));
        while(time > 0)
        {
            yield return new WaitForSeconds(fireParme.duration);
            if (enemy.gameObject.activeSelf==false)
            {
                enemy.isElement = false;
                yield break;
            }
            hittable?.GetHit(fireParme.damage,null);

            time--;
        }
        enemy.isElement = false;
    }
    private IEnumerator WaterDeBuffCoroutine()
    {
        if (enemy.isElement) yield break;
        enemy.isElement = true;
        movement.ChagedMoveAgent(waterParme.decrease);
        StartCoroutine(OnElementDamagedAnimation(spriteRenderer, new Color(0, 0, 1, 1), waterParme.duration));
        yield return new WaitForSeconds(waterParme.duration);
        enemy.isElement = false;
        movement.ReturnToPreviousMoveAgent();
    }
    private IEnumerator WindDeBuffCoroutine()
    {
        
        yield return new WaitForSeconds(0f);
    }
    private IEnumerator StoneDeBuffCoroutine()
    {
        if (enemy.isElement) yield break;
        enemy.isElement = true;
        movement.ChagedMoveAgent(0);
        spriteRenderer.color = new Color(94 / 255, 94 / 255, 94 / 255, 1);
        yield return new WaitForSeconds(stoneParme.duration);
        movement.ReturnToPreviousMoveAgent();
        spriteRenderer.color = new Color(1, 1, 1, 1);
        enemy.isElement = false;
    }

    private IEnumerator OnElementDamagedAnimation(SpriteRenderer target, Color color, float duration)
    {
        yield return new WaitForSeconds(duration/ 4);
        target.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(duration / 4);
        target.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.color = new Color(1f, 1f, 1f, 1f);
    }

}
