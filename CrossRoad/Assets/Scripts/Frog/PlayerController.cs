using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private enum Direction 
    {
        Up, Right, Left
    }
    private Direction dir;
    
    private Rigidbody2D rb;
    private Animator anim;
    public float JumpDistance;
    private float moveDistance;
    private Vector2 destination;
    private Vector2 touchPosition;
    private bool buttonHold;
    private bool isJump;
    private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // // FIXME: 临时操作
        // if (destination.y - transform.position.y < 0.1) {
        //     isJump = false;
        // }
        if (canJump) 
        {
            TriggerJump();
        }
    }

    private void FixedUpdate() 
    {
        if (isJump) 
        {
            rb.position = Vector2.Lerp(transform.position, destination, 0.134f);   
        }
        
    }

    #region INPUT 输入回调函数
    public void Jump(InputAction.CallbackContext context)
   {
        // TODO:执行跳跃，跳跃的距离，记录分数，播放跳跃的音效
        if (context.performed && !isJump)
        {
            moveDistance = JumpDistance;
            // Debug.Log("Jump!" + "  " + moveDistance);
            // 执行跳跃
            // destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            canJump = true;
        }
        
   }

   public void LongJump(InputAction.CallbackContext context)
   {
        if (context.performed && !isJump)
        {
            moveDistance = JumpDistance * 2;
            buttonHold = true;
            // Debug.Log("Long jump 000!" + "  " + moveDistance);
            
        
        }

        if (context.canceled && buttonHold && !isJump) 
        {
            // 执行跳跃

            buttonHold = false;
            // Debug.Log("Long jump 111!" + "  " + moveDistance);
            // destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            canJump = true;
        }
        
   }

    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        
          if (context.performed) 
          {
            // Debug.Log(context.ReadValue<Vector2>());
            touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            // Debug.Log(touchPosition);
            var offset = ((Vector3) touchPosition - transform.position).normalized;
            if (Mathf.Abs(offset.x) <= 0.7f) 
            {
                dir = Direction.Up;
            }
            else if (offset.x < 0f) 
            {
                dir = Direction.Left;
            }
            else if (offset.x > 0f) 
            {
                dir = Direction.Right;
            }
          }

    }
   
   #endregion

    #region 动画
    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump() 
    {
        //todo:: 获得移动方向，播放动画
        canJump = false;
        anim.SetTrigger("Jump");

        switch(dir)
        {
            case Direction.Up:
                // TODO:触发切换动画
                anim.SetBool("isSide", false);
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                break;
            case Direction.Left:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                break;
            case Direction.Right:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                break;
        }
        
    }

    public void JumpAnimationEvent() 
    {
        // 改变状态
        isJump = true;
        
    }

    public void FinishJumpAnimationEvent()
    {
        isJump = false;
    }
    #endregion
}
