using NUnit.Framework;
using UnityEngine;

public class ScoreTest
{
    [Test]
    public void AddToScore()
    {
        PlayerStats player = new PlayerStats();
        player.AddToScoreTest(25);
        Assert.AreEqual(25, player.scoreTest);
    }
}
