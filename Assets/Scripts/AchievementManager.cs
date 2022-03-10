using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    //Ich benutze hier c# events bzw. das Observer Pattern. Ich übergebe dabei ein AchievementType and den UIManager bzw wer alles zuhört. Ziel ist es,
    //dass der UIManager und der AchievementManager nichts voneinander wissen müssen.

    public delegate void OnAchievementUnlocked(AchievementType type);
    public static event OnAchievementUnlocked unlockAchievement;

    #region FrutiAchievements
    //FruitAchievements
    private bool appleAchievementUnlocked = false;
    private bool bananaAchievementUnlocked = false;
    private bool MelonAchievementUnlocked = false;
    [SerializeField] private int applesNeeded;
    [SerializeField] private int bananasNeeded;
    [SerializeField] private int melonsNeeded;
    private int apples;
    private int bananas;
    private int melons;
    #endregion
    #region EnemiesAchievements
    //EnemiesAchievements
    private bool enemiesKilledAchievementUnlocked = false;
    [SerializeField] private int killedEnemiesNeeded;
    private int enemiesKilled;
    #endregion
    #region ExplosionAchievements
    //ExplosionAchievements
    private bool explosionAchievementUnlocked = false;
    [SerializeField] private int explosionsNeeded;
    private int explosions;
    #endregion
    #region TutorialAchievement
    //TutorialAchievement
    private bool tutorialAchievementUnlocked = false;
    [SerializeField] private int tutorialsNeeded;
    private int tutorial = 0;
    #endregion
    private void Start()
    {
        Apple.addToAchievement += AddToApples;  //subscriben  
        Banana.addToAchievement += AddToBananas;//subscriben  
        Melon.addToAchievement += AddToMelons;  //subscriben 

        EnemyStats.onEnemyKilled += AddToEnemyKilled;

        Bomb.onExplosion += AddToExplosions;

        TutorialEvent.onAddToAchievement += AddToTutorial;
    }
    private void AddToApples(int amount)
    {
        apples += amount;
        if (apples >= applesNeeded && !appleAchievementUnlocked)
        {
            appleAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.apple);    //shout out
        }
    }
    private void AddToBananas(int amount)
    {
        bananas += amount;
        if (bananas >= bananasNeeded && !bananaAchievementUnlocked)
        {
            bananaAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.banana);   //shout out
        }
    }
    private void AddToMelons(int amount)
    {
        melons += amount;
        if (melons >= melonsNeeded && !MelonAchievementUnlocked)
        {
            MelonAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.melon);    //shout out
        }
    }

    private void AddToEnemyKilled()
    {
        enemiesKilled += 1;
        if (enemiesKilled >= killedEnemiesNeeded && !enemiesKilledAchievementUnlocked)
        {
            enemiesKilledAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.enemy);
        }
    }
    private void AddToExplosions()
    {
        explosions += 1;
        if(explosions >= explosionsNeeded && !explosionAchievementUnlocked)
        {
            explosionAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.explosion);
        }
    }

    private void AddToTutorial()
    {
        tutorial += 1;
        if(tutorial >= tutorialsNeeded && !tutorialAchievementUnlocked)
        {
            tutorialAchievementUnlocked = true;
            if (unlockAchievement != null) unlockAchievement(AchievementType.tutorial);
        }
    }
}
public enum AchievementType
{
    apple,
    banana,
    melon,
    enemy,
    explosion,
    tutorial
}
