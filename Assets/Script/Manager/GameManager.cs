using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Character player;
    [SerializeField]
    private int r;
    public Vector2 maxPosition {get;private set;}
    public Vector2 minPosition {get;private set;}

    public TestScript testScript { get; private set; }
    public PlayerController playerController { get; private set; }

    public ElementManager elementManager { get; private set; }

    public CameraMove camera { get; private set; }

    private void Awake() {
        maxPosition = new Vector2(r,r);
        minPosition = new Vector2(-r,-r);
    }

    private void Start()
    {
        testScript = FindObjectOfType<TestScript>();
        playerController = FindObjectOfType<PlayerController>();
        elementManager = FindObjectOfType<ElementManager>();
        camera = FindObjectOfType<CameraMove>();
    }
    public Character PlayerInfo{
        get{ return player;}
    }
    public void ChangeHealthValue(int atk)
    {
        player.hp += atk;
    }
    public void ChangeExpValue(float exp)
    {
        player.exp += exp;
    }
    public float GetHpBar(){
        return (float)player.hp / player.maxHp;
    }
    public float GetExpValue(){
        return player.exp;
    }
}
