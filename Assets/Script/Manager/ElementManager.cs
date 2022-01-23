using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    Not,
    Fire,
    Water,
    Wind,
    Stone,
    Grass
}

[System.Serializable]
public class FireParme
{
    public float Maxduration = 3;
    public float duration = 1;
    public int damage = 1;
}

[System.Serializable]
public class WaterParme
{
    public float duration = 5;
    public float decrease = 2;
}

[System.Serializable]
public class WindParme
{
    public float thrust = 5;
    public float KnockTime = 0.3f;
}

[System.Serializable]
public class StoneParme
{
    public float duration = 5;
}

public class ElementManager : MonoBehaviour
{
    public EnemyContoller enemyContoller { get; private set; }
    [SerializeField]
    private FireParme fireParme = new FireParme();
    [SerializeField]
    private WaterParme waterParme = new WaterParme();
    [SerializeField]
    private WindParme windParme = new WindParme();
    [SerializeField]
    private StoneParme stoneParme = new StoneParme();
    [SerializeField]
    private GameObject player = null;
    public bool[] isCheck { get; private set; } = new bool[2]; //0:ºÒ, 1:¹°

    [field: SerializeField]
    public Sprite[] bulletSprite { get; private set; }

    public string[] animationString { get; private set; } = { "Fire_Animation", "Water_Drop", "Grass", "Stone" };

    [field: SerializeField]
    public Gradient[] particleG { get; private set; } 

    private void Start()
    {
        isCheck[0] = false;
        isCheck[1] = false;
    }

    public void BulletSkill(GameObject target)
    {
        enemyContoller = target.GetComponent<EnemyContoller>();
        switch(GameManager.Instance.playerController.GetCondition)
        {
            case Conditions.Fire:
                if (enemyContoller.getCondition == Conditions.Fire)
                {
                    enemyContoller.ChangeHp(-(GameManager.Instance.PlayerInfo.atk+1));
                    return;
                }
                else if (enemyContoller.getCondition == Conditions.Water) return;
                else if (enemyContoller.getCondition == Conditions.Grass)
                {
                    enemyContoller.ChangeHp(GameManager.Instance.PlayerInfo.atk);
                }
                StartCoroutine(FireBullet(enemyContoller));
                break;
            case Conditions.Water:
                if(enemyContoller.getCondition == Conditions.Fire) 
                    enemyContoller.ChangeHp(GameManager.Instance.PlayerInfo.atk);
                else if(enemyContoller.getCondition == Conditions.Water)
                {
                    enemyContoller.ChangeHp(-(GameManager.Instance.PlayerInfo.atk + 1));
                    return;
                }
                else if(enemyContoller.getCondition == Conditions.Grass) {
                    enemyContoller.ChangeHp(-(GameManager.Instance.PlayerInfo.atk + 1));
                    return;
                }
                StartCoroutine(WaterBullet(enemyContoller));
                break;
            case Conditions.Wind:
                if(enemyContoller.getCondition == Conditions.Fire)
                {
                    //enemyContoller.getcircle.SetActive(true);
                    isCheck[0] = true;

                    Invoke("OffCheck", 0.05f);
                }
                else if(enemyContoller.getCondition == Conditions.Water)
                {
                    enemyContoller.getcircle.SetActive(true);
                    isCheck[1] = true;

                    //Invoke("OffCheck", 0.01f);
                }
                else if(enemyContoller.getCondition == Conditions.Wind)
                {
                    enemyContoller.ChangeHp(-(GameManager.Instance.PlayerInfo.atk + 1));
                    return;
                }
                else if(enemyContoller.getCondition == Conditions.Stone)
                {
                    return;
                }
                WindBullet(enemyContoller);
                break;
            case Conditions.Stone:
                if(enemyContoller.getCondition == Conditions.Stone)
                {
                    return;
                }
                else
                {
                    enemyContoller.ChangeHp((GameManager.Instance.PlayerInfo.atk * 1.5f) - GameManager.Instance.PlayerInfo.atk);
                }
                StartCoroutine(StoneBullet(enemyContoller));
                break;
        }
    }

    private void OffCheck()
    {
        enemyContoller.getcircle.SetActive(false);
        isCheck[0] = false;
    }
    public IEnumerator FireBullet(EnemyContoller target)
    {
        if (target.isElement) yield break;
        target.isElement = true;
        float time = fireParme.Maxduration;
        while(time > 0)
        {
            yield return new WaitForSeconds(fireParme.duration);
            target.ChangeHp(fireParme.damage);
            if (target.gameObject.activeSelf)
            {
                int k = target.CheckHp();
                if (k == 0) yield break;
                StartCoroutine(OnElementDamagedAnimation(target, new Color(1, 0, 0, 1), fireParme.duration));
            }
            time--;
        }
    }

    private IEnumerator OnElementDamagedAnimation(EnemyContoller target, Color color, float duration)
    {
        yield return new WaitForSeconds(duration/ 4);
        target.spriteRenderer.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(fireParme.duration / 4);
        target.spriteRenderer.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        target.isElement = false;
    }

    public IEnumerator WaterBullet(EnemyContoller target)
    {
        if (target.isElement) yield break;
        target.isElement = true;
        float normal = target.getInfo.speed;
        target.ChangeSpeed(target.getInfo.speed - waterParme.decrease);
        StartCoroutine(OnElementDamagedAnimation(target, new Color(0, 0, 1, 1), waterParme.duration));
        yield return new WaitForSeconds(waterParme.duration);
        target.ChangeSpeed(normal);
    }

    public void WindBullet(EnemyContoller target)
    {
        if (target.isElement) return;
        target.isElement = true;
        //target.rigid.isKinematic = false;
        Vector2 difference = target.transform.position - player.transform.position;
        difference = difference.normalized * windParme.thrust;
        target.rigid.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockCo(target));
    }

    private IEnumerator KnockCo(EnemyContoller enemy)
    {
        if(enemy.rigid != null)
        {
            yield return new WaitForSeconds(windParme.KnockTime);
            enemy.isElement = false;
            enemy.rigid.velocity = Vector2.zero;
            //enemy.isKinematic = true;
        }
    }

    private IEnumerator StoneBullet(EnemyContoller target)
    {
        if (target.isElement) yield break;
        target.isElement = true;
        float normal = target.getInfo.speed;
        target.ChangeSpeed(0);
        target.spriteRenderer.color = new Color(94 / 255, 94 / 255, 94 / 255, 1);
        yield return new WaitForSeconds(stoneParme.duration);
        target.ChangeSpeed(normal);
        target.spriteRenderer.color = new Color(1, 1, 1, 1);
        target.isElement = false;
    }
}
