using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        
        actions.anim.ResetTrigger("Reload");
        actions.anim.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager actions)
    {
        
    }
}
