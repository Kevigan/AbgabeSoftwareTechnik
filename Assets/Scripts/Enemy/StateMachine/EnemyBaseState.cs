using UnityEngine;

public abstract class EnemyBaseState
{
    protected bool battleSoundStarted = false;

    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
}
