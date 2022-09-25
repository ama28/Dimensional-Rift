using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStateType {
        MainMenu, BuildPhase, ActionPhase, GameOver
    };
    
    public GameStateType GameState { //use GameState to change state of game
        get { return gameState; } 
        set { SetGameState(value); }
    }
    private GameStateType gameState;

    public SpawnManager spawnManager;

    [SerializeField] WaveList waveList; 

    public int Round => round; //Round is read-only outside of GameManager - shorthand for get & no set
    private int round = 0;

    public int Level => level;
    private int level = 1;

    public int currency = 0;

    //Events
    //These events will be called when the game state is changed. When an event is called, all subscribed
    //functions fire. To subscribe a function to an event, write "EventName += FnName" inside an OnEnable 
    //function, and "EventName -= FnName" inside an OnDisable() function.
    public static event Action OnBuildPhaseStart;
    public static event Action<Wave> OnActionPhaseStart;
    public static event Action OnGameOver;


    void Start() {
        SetGameState(GameStateType.ActionPhase);
    }

    //Fire proper events on state change
    public void SetGameState(GameStateType newState) {
        if(newState == gameState) return;

        switch(newState) {
            case(GameStateType.MainMenu): {
                //TODO: load menu scene
                break;
            }
            case(GameStateType.BuildPhase): {
                level++;
                OnBuildPhaseStart?.Invoke();
                break;
            }
            case(GameStateType.ActionPhase): {
                OnActionPhaseStart?.Invoke(waveList.waves[round]);
                break;
            }
            case(GameStateType.GameOver): {
                OnGameOver?.Invoke();
                break;
            }
        }

        gameState = newState;
    }
}
