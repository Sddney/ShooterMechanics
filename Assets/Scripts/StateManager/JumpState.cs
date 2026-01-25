using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : BaseState
{

    public override void EnterState(Movement movement)
    {
      
      if(movement.previousState == movement.idle) movement.animator.SetTrigger("IdleJump");
      else if (movement.previousState == movement.walk || movement.previousState == movement.run) movement.animator.SetTrigger("RunJump");

    }

    public override void UpdateState(Movement movement)
    {
        if(movement.jumped == true && movement.IsGrounded())
        {
            movement.jumped = false;
            if(movement.xInput == 0 && movement.zInput == 0) movement.ChangeState(movement.idle);
            else if (Input.GetKey(KeyCode.LeftShift)) movement.ChangeState(movement.run);
            else movement.ChangeState(movement.walk);
        }
    }

}
