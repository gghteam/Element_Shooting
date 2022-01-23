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
    public Enemy Enemy { get; private set; }
    private AgentMovement movement;
    private SpriteRenderer spriteRenderer;
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
    public bool[] isCheck { get; private set; } = new bool[2]; //0:��, 1:��

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

    public void BulletSkill(GameObject target,GameObject dealer)
    {
        IHittable hittable = target.GetComponent<IHittable>();
        Enemy = target.GetComponent<Enemy>();
        movement = target.GetComponent<AgentMovement>();
        spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
        switch(GameManager.Instance.playerController.GetCondition)
        {
            case Conditions.Fire:
                if (Enemy.getCondition == Conditions.Fire)
                {
                    hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                    return;
                }
                else if (Enemy.getCondition == Conditions.Water) return;
                else if (Enemy.getCondition == Conditions.Grass)
                {
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk*2,dealer);
                }else{
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                }
                //StartCoroutine(FireBullet(Enemy));
                break;
            case Conditions.Water:
                if(Enemy.getCondition == Conditions.Fire) 
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk*2,dealer);
                else if(Enemy.getCondition == Conditions.Water)
                {
                    hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                    return;
                }
                else if(Enemy.getCondition == Conditions.Grass) {
                    hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                    return;
                }
                else
                {
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                }
                //StartCoroutine(WaterBullet(Enemy,movement));
                break;
            case Conditions.Wind:
                if(Enemy.getCondition == Conditions.Fire)
                {
<<<<<<< HEAD
                    //enemyContoller.getcircle.SetActive(true);
=======
                    //Enemy.getcircle.SetActive(true);
>>>>>>> AI
                    isCheck[0] = true;

                    Invoke("OffCheck", 0.05f);
                }
                else if(Enemy.getCondition == Conditions.Water)
                {
                    //Enemy.getcircle.SetActive(true);
                    isCheck[1] = true;

                    //Invoke("OffCheck", 0.01f);
                }
                else if(Enemy.getCondition == Conditions.Wind)
                {
                    hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                    return;
                }
                else if(Enemy.getCondition == Conditions.Stone)
                {
                    return;
                }
                else{
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                }
                WindBullet(Enemy);
                break;
            case Conditions.Stone:
                if(Enemy.getCondition == Conditions.Stone)
                {
                    return;
                }
                else
                {
                    hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                }
                //StartCoroutine(StoneBullet(Enemy));
                break;
        }
    }

    private void OffCheck()
    {
        //Enemy.getcircle.SetActive(false);
        isCheck[0] = false;
    }
    public IEnumerator FireBullet(Enemy target,SpriteRenderer spriteRenderer)
    {
        IHittable hittable = target.GetComponent<IHittable>();
        if (target.isElement) yield break;
        target.isElement = true;
        float time = fireParme.Maxduration;
        while(time > 0)
        {
            yield return new WaitForSeconds(fireParme.duration);
            if (target.gameObject.activeSelf==false)
            {
                target.isElement = false;
                yield break;
            }
            hittable?.GetHit(fireParme.damage,null);
            StartCoroutine(OnElementDamagedAnimation(spriteRenderer, new Color(1, 0, 0, 1), fireParme.duration));
            time--;
        }
        target.isElement = false;
    }

    private IEnumerator OnElementDamagedAnimation(SpriteRenderer target, Color color, float duration)
    {
        yield return new WaitForSeconds(duration/ 4);
        target.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(fireParme.duration / 4);
        target.color = color;
        yield return new WaitForSeconds(duration / 4);
        target.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator WaterBullet(Enemy target,AgentMovement movement,SpriteRenderer spriteRenderer)
    {
        if (target.isElement) yield break;
        target.isElement = true;
        //float normal = target.;
        movement.ChagedMoveAgent(waterParme.decrease);
        StartCoroutine(OnElementDamagedAnimation(spriteRenderer, new Color(0, 0, 1, 1), waterParme.duration));
        yield return new WaitForSeconds(waterParme.duration);
        target.isElement = false;
        movement.ReturnToPreviousMoveAgent();
    }

    public void WindBullet(Enemy target)
    {
        if (target.isElement) return;
        target.isElement = true;
        //target.rigid.isKinematic = false;
        Vector2 difference = target.transform.position - player.transform.position;
        difference = difference.normalized * windParme.thrust;
        //target.rigid.AddForce(difference, ForceMode2D.Impulse);
        //StartCoroutine(KnockCo(target));
    }

    // private IEnumerator KnockCo(Enemy enemy)
    // {
    //     if(enemy.rigid != null)
    //     {
    //         yield return new WaitForSeconds(windParme.KnockTime);
    //         enemy.isElement = false;
    //         enemy.rigid.velocity = Vector2.zero;
    //         //enemy.isKinematic = true;
    //     }
    // }

    private IEnumerator StoneBullet(Enemy target,AgentMovement movement,SpriteRenderer spriteRenderer)
    {
        if (target.isElement) yield break;
        target.isElement = true;
        movement.ChagedMoveAgent(0);
        spriteRenderer.color = new Color(94 / 255, 94 / 255, 94 / 255, 1);
        yield return new WaitForSeconds(stoneParme.duration);
        movement.ReturnToPreviousMoveAgent();
        spriteRenderer.color = new Color(1, 1, 1, 1);
        target.isElement = false;
    }
}
