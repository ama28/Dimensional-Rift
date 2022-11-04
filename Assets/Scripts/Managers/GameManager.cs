using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Managers")]
    public SpawnManager spawnManager;
    public BuildingManager BuildingManager;

    [Header("Misc Info")]
    [SerializeField] WaveList waveList; 

    // public int Round => round; 
    // private int round = 0;

    public int Level => level; //Round is read-only outside of GameManager - shorthand for get & no set
    private int level = 0;

    public int currency = 0;

    public float samXPosition;

    //Events
    //These events will be called when the game state is changed. When an event is called, all subscribed
    //functions fire. To subscribe a function to an event, write "EventName += FnName" inside an OnEnable 
    //function, and "EventName -= FnName" inside an OnDisable() function.
    public static event Action OnBuildPhaseStart;
    public static event Action<Wave> OnActionPhaseStart;
    public static event Action OnGameOver;
    public static event Action OnMainMenu;
    public static event Action OnRestart; //should only be used to destroy singletons

    public PlayerFarmer playerFarmer;
    public PlayerShooter playerShooter;

    public override void Awake() {
        base.Awake();
        playerFarmer = FindObjectOfType<PlayerFarmer>();
        playerShooter = FindObjectOfType<PlayerShooter>();
    }

    void Start() {
        SetGameState(GameStateType.BuildPhase);
        Time.timeScale = 1f;
    }

    public void Restart() {
        currency = 0;
        level = 0;
        OnRestart?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerFarmer = FindObjectOfType<PlayerFarmer>();
        playerShooter = FindObjectOfType<PlayerShooter>();
        SetGameState(GameStateType.BuildPhase);
    }

    void Update() {

        //for debug TODO CHANGE
        if(Input.GetKeyDown(KeyCode.H) && BuildingManager.GetNumBuildingsInInventory() == 0) {
            if(gameState == GameStateType.BuildPhase) {
                Debug.Log("starting level " + level);
                SetGameState(GameStateType.ActionPhase);
            }
        }
        samXPosition = GameObject.FindGameObjectWithTag("PlayerShooter").transform.position.x;
    }

    //Fire proper events on state change
    public void SetGameState(GameStateType newState) {
        if(newState == gameState) return;

        switch(newState) {
            case(GameStateType.MainMenu): {
                //TODO: load menu scene
                OnMainMenu?.Invoke();
                break;
            }
            case(GameStateType.BuildPhase): {
                level++;
                OnBuildPhaseStart?.Invoke();
                break;
            }
            case(GameStateType.ActionPhase): {
                OnActionPhaseStart?.Invoke(waveList.waves[level]);
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
