using UnityEngine;

public class EnemyRelaxedState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.RelaxedSymbol.SetActive(true);
        enemy.enemy.enabled = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.distance <= enemy.distanceAlerted)
        {
            enemy.SwitchState(enemy.alertedState);
            enemy.RelaxedSymbol.SetActive(false);
        }

    }
}
