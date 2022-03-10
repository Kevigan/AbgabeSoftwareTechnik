using UnityEngine;
using NUnit.Framework;

public class TakeDamageTest
{
    [Test]
    public void TakeDamageBarrel()      
    {
        Barrel barrel = new Barrel();
        barrel.TakeDamageTest(50);
        Assert.AreEqual(-30, barrel.lifeTest);
    }
    [Test]
    public void TakeDamagePlayer()
    {
        PlayerStats player = new PlayerStats();
        player.TakeDamageTest(10);
        Assert.AreEqual(90, player.lifePointsTest);
    }

    [Test]
    public void TakeDamageEnemy()
    {
        EnemyStats enemy = new EnemyStats();
        enemy.TakeDamageTest(10);
        Assert.AreEqual(90, enemy.lifePointsTest);
    }
}
