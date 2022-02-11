using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private Character player;
    [SerializeField]
    private int r;
    public Vector2 maxPosition {get;private set;}
    public Vector2 minPosition {get;private set;}

    public PlayerController playerController { get; private set; }

    public ElementManager elementManager { get; private set; }

    public UiManager uiManager { get; private set; }

    public new CameraMove camera { get; private set; }

    public DialogueManager dialogueManager { get; private set; }

    public Rebound rebound { get; private set; }

    public DamagePopup damagePopup { get; private set; }

    public Shield shield { get; private set; }

    public StoryData storyData { get; private set; }

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("Multiple Gamemanager is running");
        }
        Instance = this;
        maxPosition = new Vector2(r,r);
        minPosition = new Vector2(-r,-r);
        dialogueManager = FindObjectOfType<DialogueManager>();
        storyData = FindObjectOfType<StoryData>();
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        elementManager = FindObjectOfType<ElementManager>();
        camera = FindObjectOfType<CameraMove>();
        uiManager = FindObjectOfType<UiManager>();
        rebound = FindObjectOfType<Rebound>();
        damagePopup = FindObjectOfType<DamagePopup>();
        shield = FindObjectOfType<Shield>();
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
