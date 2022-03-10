using UnityEngine;

public class EnemyLongRangeState : EnemyBaseState
{
    private float timerBetweenShots = 2f;
    private bool canShoot = true;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.LongRangeAttackSymbol.SetActive(true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.transform.LookAt(enemy.Player);

        CalculateTimerBetweenShots();

        if (canShoot)
        {
            GameObject bullet = enemy.objectPooler.SpawnFromPool("EnemyBullet", enemy.bulletSpawnPos.position, Quaternion.identity);
            bullet.transform.rotation = enemy.transform.rotation;
            canShoot = false;
        }

        if (enemy.distance > enemy.distanceToAttackRange)
        {
            enemy.SwitchState(enemy.alertedState);
            enemy.LongRangeAttackSymbol.SetActive(false);
        }
        else if (enemy.distance <= enemy.distanceToAttackMelee)
        {
            enemy.enemy.enabled = true;
            enemy.SwitchState(enemy.meleeState);
            enemy.LongRangeAttackSymbol.SetActive(false);
        }
    }

    void CalculateTimerBetweenShots()
    {
        timerBetweenShots -= Time.deltaTime;
        if (timerBetweenShots <= 0)
        {
            canShoot = true;
            timerBetweenShots = 2f;
        }
    }
}
