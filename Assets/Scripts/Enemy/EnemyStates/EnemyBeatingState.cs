using UnityEngine;

public class EnemyBeatingState : EnemyBaseState
{
    
    public override void EnterState(EnemyMovement enemy)
    {
        Debug.Log("Enemy is beating");
        enemy.animator.SetTrigger("Punch");
    }

    public override void UpdateState(EnemyMovement enemy)
    {
        
    }
}
