using UnityEngine;

public class EnemyMeleeState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.MeleeAttackSymbol.SetActive(true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.distance <= enemy.distanceToAttackMelee)
        {
            enemy.enemy.SetDestination(enemy.Player.position);
        }

        if (enemy.distance > enemy.distanceToAttackMelee)
        {
            enemy.SwitchState(enemy.longRangeState);
            enemy.MeleeAttackSymbol.SetActive(false);

            enemy.enemy.enabled = false;
        }
    }


}
