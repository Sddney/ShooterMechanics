using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(Movement movement);
    public abstract void UpdateState(Movement movement);
}
