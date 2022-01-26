using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Flags]
    private enum PlayerState
    {
        NONE = 0,
        RUN = 1 << 0,
        DAMAGED = 2 << 1,
        Attack = 3 << 2

    }
  
    private PlayerState state = PlayerState.NONE;

    private float moveSpeed = 0;
    private float velocityX = 0;
    private float velocityY = 0;

    private readonly int _walkHashStr = Animator.StringToHash("Walk");
    private readonly int _deathHashStr = Animator.StringToHash("Death");
    private readonly int _attackHashStr = Animator.StringToHash("Attack");
    private readonly int _damagedHashStr = Animator.StringToHash("Damaged");

    private Collider2D col = null;
    private Rigidbody2D playerRigid = null;
    private Animator animator = null;
    private Camera Camera = null;
    private void Start() {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        col = GetComponent<Collider2D>();
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update() {
        SetCharacterDirection();
        if (GameManager.Instance.shield.isAni) return;
        else Move();
        Dash();
        
    }
    public void Death()
    {
        animator.SetTrigger(_deathHashStr);
    }
    public void Damaged()
    {
        state = PlayerState.DAMAGED;
        DamagedAnimation();
    }
    public void Attack()
    {
        state = PlayerState.Attack;
        AttackAnimation(true);
    }
    public void UnAttack()
    {
        AttackAnimation(false);
    }
    private void Idle()
    {
        state = PlayerState.NONE;
    }
    private void Move()
    {
        if (GameManager.Instance.shield.isAni) return;
        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");
        if(state.HasFlag(PlayerState.RUN))
        {
            moveSpeed = GameManager.Instance.PlayerInfo.speed * 2;
        }
        else
        {
            moveSpeed = GameManager.Instance.PlayerInfo.speed;
        }
        playerRigid.velocity = new Vector2(velocityX, velocityY).normalized * moveSpeed;
    }
    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool(_walkHashStr,true);
            state = PlayerState.RUN;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool(_walkHashStr,false);
            Idle();
        }
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
