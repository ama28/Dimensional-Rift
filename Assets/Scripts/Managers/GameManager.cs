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

    public uint Round => round; //Round is read-only outside of GameManager - shorthand for get & no set
    private uint round = 0;

    //Events
    //These events will be called when the game state is changed. When an event is called, all subscribed
    //functions fire. To subscribe a function to an event, write "EventName += FnName" inside an OnEnable 
    //function, and "EventName -= FnName" inside an OnDisable() function.
    public static event Action OnBuildPhaseStart;
    public static event Action OnActionPhaseStart;
    public static event Action OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Fire proper events on state change
    void SetGameState(GameStateType newState) {
        if(newState == gameState) return;

        switch(newState) {
            case(GameStateType.MainMenu): {
                //TODO: load menu scene
                break;
            }
            case(GameStateType.BuildPhase): {
                OnBuildPhaseStart?.Invoke();
                break;
            }
            case(GameStateType.ActionPhase): {
                OnActionPhaseStart?.Invoke();
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
