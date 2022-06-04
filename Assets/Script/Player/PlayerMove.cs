using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Flags]
    private enum PlayerMotionState
    {
        NONE = 0,
        RUN = 1 << 0,
        DAMAGED = 2 << 1,
        Attack = 3 << 2,
        Death = 4 << 3

    }
  
    private PlayerMotionState state = PlayerMotionState.NONE;

    [SerializeField]
    private float decreaseSpeed = 2f;
    private float _stamina = 0f;
    private float _maxStamina = 0f;
    private float moveSpeed = 0f;
    private float velocityX = 0f;
    private float velocityY = 0f;

    private readonly int _walkHashStr = Animator.StringToHash("Walk");
    private readonly int _deathHashStr = Animator.StringToHash("Death");
    private readonly int _attackHashStr = Animator.StringToHash("Attack");
    private readonly int _damagedHashStr = Animator.StringToHash("Damaged");

    private Collider2D col = null;
    private Rigidbody2D playerRigid = null;
    private Animator animator = null;
    private Camera Camera = null;
    private PlayerState _playerState;

    private bool _isDead = false;
    private bool _isRun = false;
    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
    }
    private void Start() {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        col = GetComponent<Collider2D>();
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _stamina = _playerState.CharacterState.maxStamina;

        //Debug.Log(GameManager.Instance.SetPos);
        transform.position = GameManager.Instance.SetPos;
    }
    private void Update() {
        SetCharacterDirection();
        if (PlayerPrefs.GetInt("TURORIAL", 1) == 1)
        {
            if (GameManager.Instance.IsStopEvent || GameManager.Instance.dialogueManager.IsDialogue)
            {
                playerRigid.velocity = Vector2.zero;
            }
            else Move();
        }
        else {
            Move();
                }
        Dash();
        Run();
        StaminaRecovery();
    }
    public void SetStamina(float value)
    {
        _maxStamina = value;
    }
    public void Death()
    {
        _isDead =true;
        animator.SetTrigger(_deathHashStr);
    }
    public void Damaged()
    {
        state = PlayerMotionState.DAMAGED;
        DamagedAnimation();
    }
    public void Attack()
    {
        state = PlayerMotionState.Attack;
        AttackAnimation(true);
    }
    public void UnAttack()
    {
        AttackAnimation(false);
    }
    private void Idle()
    {
        _isRun = false;
        animator.SetBool(_walkHashStr,false);
        state = PlayerMotionState.NONE;
    }
    private void Move()
    {
        if(_isDead)return;
        if (PlayerPrefs.GetInt("TURORIAL", 1) == 1)
        {
            if (GameManager.Instance.IsStopEvent) return;
        }
        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");
        if(state.HasFlag(PlayerMotionState.RUN))
        {
            moveSpeed = _playerState.CharacterState.speed * 2;
        }
        else
        {
            moveSpeed = _playerState.CharacterState.speed;
        }
        playerRigid.velocity = new Vector2(velocityX, velocityY).normalized * moveSpeed;
    }
    private void Dash()
    {
        if(_stamina>0)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isRun = true;
                animator.SetBool(_walkHashStr,true);
                state = PlayerMotionState.RUN;
            }
        }
        else
            Idle();
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Idle();
        }
    }
    private void Run(){
        if(!_isRun)return;
        _stamina -= Time.deltaTime *  decreaseSpeed;
        if(_stamina<0)
        {
            _stamina = 0;
        }
        GameManager.Instance.ChangeStaminaValue(_stamina);
        EventManager.TriggerEvent(EventManager.EventName.PLAYER_RUN);
    }
    private void StaminaRecovery()
    {
        if(_isRun)return;
        if(_stamina> _playerState.CharacterState.maxStamina)return;
        _stamina += Time.deltaTime *  decreaseSpeed * 1.5f;
        GameManager.Instance.ChangeStaminaValue(_stamina);
        EventManager.TriggerEvent(EventManager.EventName.PLAYER_RUN);
    }
    private void DamagedAnimation()
    {
        animator.SetTrigger(_damagedHashStr);
    }
    private void AttackAnimation(bool _isSet)
    {
        animator.SetBool(_attackHashStr,_isSet);
    }
    private void SetCharacterDirection()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 vec = Camera.ScreenToWorldPoint(Input.mousePosition);
            if (vec.x > 0f)
            {
                animator.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (vec.x < 0f)
            {
                animator.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            return;
        }
        if (velocityX > 0f)
        {
            animator.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (velocityX < 0f)
        {
            animator.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

}
