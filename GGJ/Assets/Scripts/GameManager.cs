using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gamePausedCanvas;

    public PlayerManager Player;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gamePausedCanvas.SetActive(true);
            StopGame();
        }
    }

    public void GameOverScreen()
    {
        _gameOverCanvas.SetActive(true);
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0;

    }

    private void ContinueGame()
    {
        _gamePausedCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void RestartGame()
    {
        // Load scene.
    }

    public void OnContinue()
    {
        ContinueGame();
    }

    public void OnRestart()
    {
        RestartGame();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
