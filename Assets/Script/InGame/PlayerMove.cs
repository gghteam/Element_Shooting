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
    private void Start() {
        col = GetComponent<Collider2D>();
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update() {
        SetCharacterDirection();
        Move();
        Dash();
        Damaged();
        if(Input.GetKeyDown(KeyCode.T))
        {
            animator.Play("Damaged_Player_Animation");
        }
    }
    private void Idle()
    {
        state = PlayerState.NONE;
        animator.Play("Idle");
    }
    private void Move()
    {
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
            state = PlayerState.RUN;
            animator.Play("Run");
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Idle();
        }
    }
    private void Damaged()
    {
        if(state.HasFlag(PlayerState.DAMAGED))
        {
            animator.Play("Damaged_Player_Animation");
        }
    }
    private void SetCharacterDirection()
    {
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
