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

        
       
        if(Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.ammo.Reload();
            actions.rHandAim.weight = Mathf.Lerp(actions.rHandAim.weight, 0, Time.deltaTime * 10);
            actions.LHandIK.weight = Mathf.Lerp(actions.LHandIK.weight, 0, Time.deltaTime * 10);
            Debug.Log("ChangedToReaload");
            actions.ChangeState(actions.reloadState);
            
            
        }
        else if (Input.mouseScrollDelta.y != 0)
        {
            scrollDirection = Input.mouseScrollDelta.y;
            actions.ChangeState(actions.swap);

            Debug.Log("ChangedToSwap");
        }
    }

    public bool CanReload(ActionStateManager actions)
    {
         
        if(actions.ammo.currentAmmo == actions.ammo.clipSize) return false;
        if(actions.ammo.reserveAmmo == 0) return false;
        return true;

    }
}
