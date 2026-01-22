using UnityEngine;

public class Crouch : BaseState
{
    public override void EnterState(Movement movement)
    {
        movement.animator.SetBool("Crouching", true);
    }

    public override void UpdateState(Movement movement)
    {
        if(Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        else if(Input.GetKeyDown(KeyCode.C)) 
        {
            if(movement.direction.magnitude < 0.01f) ExitState(movement, movement.idle);
            else ExitState(movement, movement.walk);
        }

        if(movement.zInput > 0) movement.currentSpeed = movement.crouchSpeed;
        else if(movement.zInput < 0) movement.currentSpeed = movement.crouchBackSpeed;
    }

    void ExitState(Movement movement, BaseState state)
    {
        movement.animator.SetBool("Crouching", false);
        movement.ChangeState(state); 
    }
}
