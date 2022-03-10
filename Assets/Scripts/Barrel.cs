using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamageable
{
    private int life = 20;

    #region UnitTesting
    [HideInInspector]
    public int lifeTest = 20;
    #endregion

    public void TakeDamage(int value)
    {
        life -= value;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    #region UnitTestingMethods
    public void TakeDamageTest(int value)
    {
        lifeTest -= value;
    }
    #endregion
}
