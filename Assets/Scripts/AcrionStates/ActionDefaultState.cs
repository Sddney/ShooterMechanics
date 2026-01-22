using UnityEngine;

public class ActionDefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.rHandAim.weight = 1;
        actions.LHandIK.weight = 1;
    }

    public override void UpdateState(ActionStateManager actions)
    {
        actions.rHandAim.weight = Mathf.Lerp(actions.rHandAim.weight, 1, Time.deltaTime * 10);
        actions.LHandIK.weight = Mathf.Lerp(actions.LHandIK.weight, 1, Time.deltaTime * 10);

       
        if(Input.GetKeyDown(KeyCode.R) && CanReload(actions) == true)
        {
            actions.ChangeState(actions.reloadState);
        }
    }

    bool CanReload(ActionStateManager actions)
    {
       if(actions.ammo.currentAmmo == actions.ammo.clipSize) return false;
       else if (actions.ammo.reserveAmmo == 0) return false;
       else return true;
        
    }
}
