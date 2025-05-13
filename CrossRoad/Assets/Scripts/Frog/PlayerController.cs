using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpDistance;
    private float moveDistance;

    private bool buttonHold;

    private void Awake()
    {
        
    }
    public void Jump(InputAction.CallbackContext context)
   {
        // TODO:执行跳跃，跳跃的距离，记录分数，播放跳跃的音效
        if (context.performed)
        {
            moveDistance = JumpDistance;
            // Debug.Log("Jump!" + "  " + moveDistance);
            // 执行跳跃
        }
        
   }

   public void LongJump(InputAction.CallbackContext context)
   {
        if (context.performed) 
        {

            moveDistance = JumpDistance * 2;
            buttonHold = true;
            // Debug.Log("Long jump 000!" + "  " + moveDistance);
        }

        if (context.canceled && buttonHold) 
        {
            // 执行跳跃
            buttonHold = false;
            // Debug.Log("Long jump 111!" + "  " + moveDistance);
        }
        
   }

    public void GetTouchPosition(InputAction.CallbackContext context)
   {
      
        
   }
   
}
