using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    //public static GameManager Instance;
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



    public StoryData storyData { get; private set; }

    public ScorchParticleSystemHandler BloodParticleSystemHandler { get; private set; }
    public LoadingSceneController loadingController { get; private set; }

    private ItemInventoryCase itemInventoryCase;
    public ItemInventoryCase ItemInventoryCase
    {
        get
        {
            if(itemInventoryCase == null)
            {
                itemInventoryCase = FindObjectOfType<ItemInventoryCase>();
            }
            return itemInventoryCase;
        }
    }

    public Action OnClearAllDropItems = null; //떨어진 아이템 삭제

    private Vector2 setPos = Vector2.zero;

    private bool _isStopEvent = false; //EventStop Bool (Enemy & Player)
    public bool IsStopEvent
    {
        get => _isStopEvent;
        set
        {
            _isStopEvent = value;
        }
    }

    public Vector2 SetPos
    {
        get
        {
            return setPos;
        }
        set
        {
            if(setPos == Vector2.zero)
                setPos = value;
        }
    }

    #region PlayerState
    private int health = 0, maxHealth = 0;
    private float stamina = 0, maxStamina = 0;
    private int mana = 0;
    private float exp = 0;
    private int atk = 0;
    #endregion

    #region JSON 

    [SerializeField] 
    private User user = null;

    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }

    }
    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    #endregion

    protected override void Init()
    {
        IsStopEvent = false;
        //NOT DONDESTORY
    }

    private void Awake() {

        /*
        if(Instance != null)
        {
            Debug.LogError("Multiple Gamemanager is running");
        }
        Instance = this;
        */

        //SAVE_PATH = Application.dataPath + "/Save";
        //if (!Directory.Exists(SAVE_PATH))
        //{
        //    Directory.CreateDirectory(SAVE_PATH);
        //}
        //LoadFromJson();

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
        BloodParticleSystemHandler = FindObjectOfType<ScorchParticleSystemHandler>();
        loadingController = FindObjectOfType<LoadingSceneController>();
        if(loadingController == null)
        {
           loadingController = Instantiate(Resources.Load<LoadingSceneController>("LoadingUI")); 
        }
    }
    //public Character PlayerInfo{
    //    get{ return player;}
    //}

    public int PlayerATK
    {
        get => atk;
        set
        {
            atk = value;
        }
    }
    public int PlayerHealth
    { get => health; }
    public void ChangeHealthValue(int atk)
    {
        health += atk;
    }
    public void ChangeMaxHealthValue(int health)
    {
        maxHealth = health;
    }
    public void ChangeStaminaValue(float stamina)
    {
        this.stamina = stamina;
    }
    public void ChangeMaxStaminaValue(float stamina)
    {
        maxStamina = stamina;
    }
    public void ChangeExpValue(float exp)
    {
        this.exp += exp;
    }

    public void InitStateValue(int maxHealth,int atk,float stamina)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        PlayerATK = atk;
        this.maxStamina = stamina;
        this.stamina = stamina;
    }

    public float GetHpBar(){
        return (float)health / maxHealth;
    }
    public float GetStaminaBar(){
        return stamina / maxStamina;
    }
    public float GetExpValue(){
        return exp;
    }
}
