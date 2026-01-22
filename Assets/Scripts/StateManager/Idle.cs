using UnityEngine;

public class Idle : BaseState
{
   public override void EnterState(Movement movement)
    {
        
    }

    public override void UpdateState(Movement movement)
    {
        if (movement.direction.magnitude > 0.01f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.ChangeState(movement.run);
            else movement.ChangeState(movement.walk);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            movement.ChangeState(movement.crouch);
        }
    }
}
