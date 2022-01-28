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
        
    }

    public void OnContinue()
    {

    }

    public void OnRestart()
    {

    }

    public void OnQuit()
    {

    }


}
