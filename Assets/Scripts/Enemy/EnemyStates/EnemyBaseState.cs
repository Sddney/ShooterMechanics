using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyMovement enemy);
    public abstract void UpdateState(EnemyMovement enemy);
}
