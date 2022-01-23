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
    private void OffCheck()
    {
        //Enemy.getcircle.SetActive(false);
        isCheck[0] = false;
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
}
