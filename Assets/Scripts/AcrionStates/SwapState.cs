using System;
using UnityEngine;

public class SwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.anim.SetTrigger("SwapWeapon");
        actions.LHandIK.weight = 0;
        actions.rHandAim.weight = 0;
    }

    public override void UpdateState(ActionStateManager actions)
    {
        
    }
}