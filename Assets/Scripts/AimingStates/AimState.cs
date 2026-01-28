

using UnityEngine;

public class AimState : AimingBaseState
{
    public override void EnterState(Movement aim)
    {
        aim.animator.SetBool("Aiming", true);
        aim.currentFov = aim.aimingFov;
        aim.currentCameraX = aim.aimingCameraX;
        aim.aimPoint.SetActive(true);
        
    }
    
    public override void UpdateState(Movement aim)
    {
        if(Input.GetKeyUp(KeyCode.Mouse1)) aim.ChangeAimState(aim.aimIdle);
    }
}
