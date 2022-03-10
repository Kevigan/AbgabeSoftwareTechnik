using NUnit.Framework;
using UnityEngine;

public class HealingTest
{
    [Test]
    public void HealPlayer()
    {
        PlayerStats player = new PlayerStats();
        player.AddToLifePointsTest(25);
        Assert.AreEqual(100, player.lifePointsTest);
        player.TakeDamageTest(20);
        Assert.AreEqual(80, player.lifePointsTest);
        player.AddToLifePointsTest(50);
        Assert.AreEqual(100, player.lifePointsTest);
    }
}
