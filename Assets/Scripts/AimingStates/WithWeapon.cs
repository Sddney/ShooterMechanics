using UnityEngine;

public class WithWeapon : AimingBaseState
{
    public override void EnterState(Movement aim)
    {
        aim.animator.SetBool("Aiming", false);
        aim.currentFov = aim.idleFov;
        aim.currentCameraX = aim.idleCameraX;
    }
    
    public override void UpdateState(Movement aim)
    {
        if(Input.GetKey(KeyCode.Mouse1)) aim.ChangeAimState(aim.aim);
    }
}
