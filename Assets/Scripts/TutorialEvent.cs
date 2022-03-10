using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
    public delegate void OnStartTutorial(Texture screenShot);
    public static event OnStartTutorial onStartTutorial;

    public delegate void OnEndTutorial();
    public static event OnEndTutorial onEndTutorial;

    public delegate void OnAddToAchievement();
    public static event OnAddToAchievement onAddToAchievement;

    [SerializeField] private Texture screenShot;

    private bool eventEntered = false;

    private void Update()
    {
        gameObject.transform.Rotate(0, .1f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            onStartTutorial(screenShot);
            if (!eventEntered)
            {
                onAddToAchievement();
                eventEntered = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            onEndTutorial();
        }
    }
}
