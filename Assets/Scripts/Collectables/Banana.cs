using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Collectable
{
    //Ich benutze hier das Observer Pattern. AddToAchievement habe ich nicht in Collectable rein gelegt, da ich verschiedene
    //Früchte mit verschiedenen Achievements haben will. Ich überschreibe OntriggerEnter, behalte aber den base.

    public static event OnCollect addToAchievement;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            base.OnTriggerEnter(other);

            if (addToAchievement != null)
                addToAchievement(amount);        //shout out
        }
    }
}
