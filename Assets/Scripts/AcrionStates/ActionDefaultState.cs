using UnityEngine;

public class ActionDefaultState : ActionBaseState
{

    public float scrollDirection;
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
        else if (Input.mouseScrollDelta.y != 0)
        {
            scrollDirection = Input.mouseScrollDelta.y;
            actions.ChangeState(actions.swap);
        }
    }

    bool CanReload(ActionStateManager actions)
    {
       if(actions.ammo.currentAmmo == actions.ammo.clipSize) return false;
       else if (actions.ammo.reserveAmmo == 0) return false;
       else return true;
        
    }
}
