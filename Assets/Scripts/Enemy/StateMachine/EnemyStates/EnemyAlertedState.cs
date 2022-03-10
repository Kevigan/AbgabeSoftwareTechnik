using UnityEngine;

public class EnemyAlertedState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.AlertedSymbol.SetActive(true);
        if (!battleSoundStarted)
        {
            SoundManager.Instance.PlaySound(Sounds.battleSound);
            battleSoundStarted = true;
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.AlertedSymbol.transform.Rotate(new Vector3(0, enemy.transform.position.y, 0), 1f);

        if (enemy.distance > enemy.distanceAlerted)
        {
            enemy.SwitchState(enemy.relaxedState);
            enemy.AlertedSymbol.SetActive(false);
            battleSoundStarted = false;
            SoundManager.Instance.StopSound(Sounds.battleSound);
        }
        else if (enemy.distance <= enemy.distanceToAttackRange)
        {
            enemy.SwitchState(enemy.longRangeState);
            enemy.AlertedSymbol.SetActive(false);
        }
    }


}
