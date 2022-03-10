using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    public delegate void OnReached();
    public static event OnReached onReached;


    IEnumerator CloseApplikatioin()
    {
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() is PlayerController player)
        {
            onReached();
            player.DisableCharacterController();
            StartCoroutine(CloseApplikatioin());
            SoundManager.Instance.PlaySound(Sounds.EndGoal);
        }
    }


}
