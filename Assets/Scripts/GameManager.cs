using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStates gameState;
    private void Awake()
    {
        PlayerController.changeGameState += ChangeGameState;            //subscriben
        UIManager.changeGameState += ChangeGameState;
    }

    public void ChangeGameState(GameStates newState)
    {
        Time.timeScale = 1;

        switch (newState)
        {
            case GameStates.Playing:
                break;
            case GameStates.Pause:
                Time.timeScale = 0;
                break;
        }

        gameState = newState;
    }
}

public enum GameStates
{
    Playing,
    Pause
}