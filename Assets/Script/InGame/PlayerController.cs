using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour                          
{
    [SerializeField]
    private Transform playerPosition = null;
    private Camera Camera = null;
    private Vector2 mousePosition = Vector2.zero;
    private SpriteRenderer spriteRenderer = null;
    private HealthBar healthBar;
    private EnemyContoller enemy;
    private const float coefficient = 1;
    [SerializeField]
    private float projectileSpread;
    private int currentHp;
    private bool isDamaged = false;
    [SerializeField]
    private Conditions condition;

    public Conditions GetCondition { get { return condition; } }
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHp = GameManager.Instance.PlayerInfo.maxHp;
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        healthBar = FindObjectOfType<HealthBar>();
        StartCoroutine(Fire());
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if(isDamaged)return;
        if(collider.CompareTag("Enemy"))
        {
            enemy = collider.gameObject.GetComponent<EnemyContoller>();
            Damaged();
        }
    }
    private void Damaged(){
        isDamaged = true;
        StartCoroutine(GameManager.Instance.camera.Shake(0.2f, 0.3f));
        if(enemy != null)
        {
            GameManager.Instance.ChangeHealthValue(-enemy.Atk);
            currentHp = GameManager.Instance.PlayerInfo.hp;
            healthBar.SetHealth(currentHp);
            Debug.Log(GameManager.Instance.PlayerInfo.hp);
            if(currentHp <= 0)
            {
                OnDead();
                return;
            }
        }
        StartCoroutine(OnDamagedAnimation());
    }
    private IEnumerator OnDamagedAnimation(){
        spriteRenderer.color = new Color(0f,0f,0f,0f);
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = new Color(0f,0f,0f,0f);
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        isDamaged = false;
    }
    private void OnDead()
    {
        SceneManager.LoadScene("Menu");
    }
    private IEnumerator Fire()
    {
        while(true)
        {
            yield return null;
            if(!Input.GetMouseButton(0)) continue;
           // GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
            Vector2 v2 = Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(v2.y,v2.x) * Mathf.Rad2Deg;
            float startRotation = angle + projectileSpread / 2f;
            float angleIncrease = projectileSpread / ((float)GameManager.Instance.PlayerInfo.mul - 1f);
            for (int i = 0; i < GameManager.Instance.PlayerInfo.mul; i++)
            {
                GameObject bullet = PoolManager.Instance.GetPooledObject(0);
                if (bullet != null)
                {
                    float randomAngle;
                    if (GameManager.Instance.PlayerInfo.rpm >= 6)
                    {
                        randomAngle = Random.Range(-5f, 5f);
                    }
                    else randomAngle = 0;
                    float tempRot = i == 0 ? randomAngle + angle  : (startRotation - angleIncrease * i) + randomAngle;
                    bullet.transform.position = playerPosition.transform.position;
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, tempRot));
                    bullet.GetComponent<BulletMove>().targetPostion = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));
                    bullet.SetActive(true);
                    ChangeBulletSprite(bullet);
                }
            }
            yield return new WaitForSeconds(coefficient/(float)GameManager.Instance.PlayerInfo.rpm);
        }
        
    }

    private void ChangeBulletSprite(GameObject bullet)
    {

        bullet.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.elementManager.bulletSprite[(int)condition - 1];
        bullet.GetComponent<Animator>().Play(GameManager.Instance.elementManager.animationString[(int)condition - 1]);
    }

    public void ChangeCondition(Conditions change)
    {
        condition = change;
    }
}
