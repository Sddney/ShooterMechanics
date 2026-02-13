using UnityEngine;

public class EnemyDefaultState : EnemyBaseState
{
    
    public override void EnterState(EnemyMovement enemy)
    {
        Debug.Log("Enter Default State");
        
    }

    public override void UpdateState(EnemyMovement enemy)
    {
        //if(enemy.detection.PlayerSeen()) enemy.ChangeState(enemy.noticePlayerState);
        
    }
}
