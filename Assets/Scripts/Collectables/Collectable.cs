using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected int scorePoints;
    [SerializeField] protected int lifePoints;
    [SerializeField] protected int amount = 1;

    public delegate void OnCollect(int value);
    public static event OnCollect addToScore;
    public static event OnCollect addToLife;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if(addToScore != null)
                addToScore(scorePoints * amount);   //shout out

            if (addToLife != null)                  //shout out
                addToLife(lifePoints);

            Destroy(gameObject);
        }
    }
}
public enum CollectableTypes
{
    Apple,
    Banana,
    Melon
}
