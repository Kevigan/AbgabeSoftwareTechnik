using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public delegate void OnEnemyKilled();
    public static event OnEnemyKilled onEnemyKilled;

    [SerializeField] private Slider slider;
    [SerializeField] private float maxLifePoints = 100;
    private float lifePoints;

    #region UnitTesting
    [HideInInspector]
    public int lifePointsTest = 100;
    #endregion

    private void Start()
    {
        lifePoints = maxLifePoints;

        slider.value = CalculateHealth();
    }

    float CalculateHealth()
    {
        return lifePoints / maxLifePoints;
    }

    public void TakeDamage(int value)
    {
        lifePoints -= value;
        slider.value = CalculateHealth();
        SoundManager.Instance.PlaySound(Sounds.enemyHit);

        if (lifePoints <= 0)
        {
            onEnemyKilled();
            SoundManager.Instance.StopSound(Sounds.battleSound);
            Destroy(gameObject);
        }
    }
    #region UnitTestingMethods
    public void TakeDamageTest(int value)
    {
        lifePointsTest -= value;
    }
    #endregion
}
