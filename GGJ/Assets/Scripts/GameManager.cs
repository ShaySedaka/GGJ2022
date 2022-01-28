using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gamePausedCanvas;

    public PlayerManager Player;


    void Update()
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
        Player.FreezePlayer();
    }

    private void ContinueGame()
    {
        _gamePausedCanvas.SetActive(false);
        Time.timeScale = 1;
        Player.UnfreezePlayer();
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
