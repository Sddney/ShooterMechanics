using UnityEngine;

public class Run : BaseState
{
    public override void EnterState(Movement movement)
    {
        movement.animator.SetBool("Running", true);
    }

    public override void UpdateState(Movement movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.walk);
        else if(movement.direction.magnitude < 0.01f) ExitState(movement, movement.idle);

        if(movement.zInput > 0) movement.currentSpeed = movement.runSpeed;
        else if(movement.zInput < 0) movement.currentSpeed = movement.runBackSpeed;
    }

    void ExitState(Movement movement, BaseState state)
    {
        movement.animator.SetBool("Running", false);
        movement.ChangeState(state); 
    }
}
