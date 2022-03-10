using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public delegate void OnAddPoints(int pointsToUiScore);
    public static event OnAddPoints addToUIScore;
    public static event OnAddPoints addToUILife;

    public delegate void OnDie();
    public static event OnDie onDie;

    private int lifePoints = 100;
    private int score = 0;

    #region UnitTesting
    [HideInInspector]
    public int lifePointsTest = 100;
    [HideInInspector]
    public int scoreTest = 0;
    #endregion
    private void Start()
    {
        Collectable.addToScore += AddToScore;       //subscriben
        Collectable.addToLife += AddToLifePoints;   //subscriben

        AddToUiLife();
        AddToScore(score);
    }
    void AddToScore(int value)
    {
        this.score += value;
        if (addToUIScore != null)
        {
            addToUIScore(score);
        }
    }

    void AddToLifePoints(int value)
    {
        lifePoints += value;
        if (lifePoints >= 100)
            lifePoints = 100;
        AddToUiLife();
    }

    void AddToUiLife()
    {
        if (addToUILife != null)
        {
            addToUILife(lifePoints);
        }
    }

    private void OnDestroy()
    {
        Collectable.addToScore -= AddToScore;       //deSubscriben
        Collectable.addToLife -= AddToLifePoints;   //deSubscriben
    }

    public void TakeDamage(int value)
    {
        SoundManager.Instance.PlaySound(Sounds.playerHit);
        lifePoints -= value;
        AddToUiLife();
        if(lifePoints <= 0)
        {
            lifePoints = 100;
            addToUILife(lifePoints);
            onDie();
        }
    }
    #region UnitTestingMethods
    public void TakeDamageTest(int value)
    {
        lifePointsTest -= value;
    }
    public void AddToLifePointsTest(int value)
    {
        lifePointsTest += value;
        if (lifePointsTest >= 100)
            lifePointsTest = 100;
    }
    public void AddToScoreTest(int value)
    {
        this.scoreTest += value;
    }
    #endregion
}
