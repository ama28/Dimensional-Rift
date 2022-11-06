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

    public GameObject mainUI;

    public override void Awake() {
        if(SceneManager.GetActiveScene().name == "MainScene") {
            MainSceneSetup();
        }
        base.Awake();
    }

    void MainSceneSetup() {
        //these use the GameManager.Instance because they'll be running in the Awake() of the instance that will be deleted
        GameManager.Instance.playerFarmer = FindObjectOfType<PlayerFarmer>();
        GameManager.Instance.playerShooter = FindObjectOfType<PlayerShooter>();
        GameManager.Instance.spawnManager = GameManager.Instance.GetComponent<SpawnManager>();
        GameManager.Instance.BuildingManager = FindObjectOfType<BuildingManager>();
        GameManager.Instance.SetGameState(GameStateType.BuildPhase);
        GameManager.Instance.mainUI = GameObject.FindGameObjectWithTag("MainUI");
        Time.timeScale = 1f;
    }

    public void Restart() {
        currency = 100;
        level = 0;
        OnRestart?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //TODO: make one function, am currently too lazy to replace the inspector references
    public void StartGame() {
        currency = 0;
        level = 0;
        OnRestart?.Invoke();
        SceneManager.LoadScene("MainScene");
    }

    void Update() {

        //for debug TODO CHANGE
        if(Input.GetKeyDown(KeyCode.H) && BuildingManager.GetNumBuildingsInInventory() == 0) {
            if(gameState == GameStateType.BuildPhase) {
                Debug.Log("starting level " + level);
                SetGameState(GameStateType.ActionPhase);
            }
        }
    }

    //Fire proper events on state change
    public void SetGameState(GameStateType newState) {
        if(newState == gameState) return;

        gameState = newState;
        switch(newState) {
            case(GameStateType.MainMenu): {
                SceneManager.LoadScene("MainMenu");
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
    }

}
