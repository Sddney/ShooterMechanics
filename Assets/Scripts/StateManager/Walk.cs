using UnityEngine;

public class Walk : BaseState
{
    public override void EnterState(Movement movement)
    {
        movement.animator.SetBool("Walking", true);
    }

    public override void UpdateState(Movement movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        else if(Input.GetKeyDown(KeyCode.C)) ExitState(movement, movement.crouch);
        else if (movement.direction.magnitude < 0.01f) ExitState(movement, movement.idle);

        if(movement.zInput > 0) movement.currentSpeed = movement.walkSpeed;
        else if(movement.zInput < 0) movement.currentSpeed = movement.walkBackSpeed;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.jump); 
        }
    }


    void ExitState(Movement movement, BaseState state)
    {
        movement.animator.SetBool("Walking", false);
        movement.ChangeState(state); 
    } 
}
