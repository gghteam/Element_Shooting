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
        Move();
        Dash();
        
    }
    public void Damaged()
    {
        state = PlayerState.DAMAGED;
        DamagedAnimation();
    }
    public void Attack()
    {
        state = PlayerState.Attack;
        AttackAnimation();
    }
    public void ReturnToPreviousState()
    {
        state = PlayerState.NONE;
    }
    private void Idle()
    {
        state = PlayerState.NONE;
        animator.Play("Idle");
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
        if(Input.GetKeyDown(KeyCode.LeftShift)&&state.HasFlag(PlayerState.Attack))
        {
            state = PlayerState.RUN;
            animator.Play("Attack_Player_Animation");
        }else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = PlayerState.RUN;
            animator.Play("Run");
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Idle();
        }
    }
    private void DamagedAnimation()
    {
        if(state.HasFlag(PlayerState.DAMAGED))
        {
            animator.Play("Damaged_Player_Animation");
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged_Player_Animation"))
            {
                state &= ~PlayerState.DAMAGED;
            }
        }
    }
    private void AttackAnimation()
    {
        if(state.HasFlag(PlayerState.Attack))
        {
            animator.Play("Attack_Player_Animation");
            state &= ~PlayerState.Attack;
        }
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
