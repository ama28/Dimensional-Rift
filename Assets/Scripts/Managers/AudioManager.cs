using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager i;
    private static FMOD.Studio.EventInstance Music;

    void Awake()
     {
         if(i != null) {
            GameObject.Destroy(this);
         }
         else {
            i = this;

            DontDestroyOnLoad(this);
         }

     }

    void State()
    {
        StartMusic();
    }

     public void StartMusic() {
        StopMusic();
        //switch music using game state
        string eventString;
        switch(GameManager.GameState)
        { 
            case (GameManager.GameStateType.MainMenu):
                eventString = "event:/Music/Main Menu";
                break;
            case (GameManager.GameStateType.BuildPhase):
                eventString = "event:/Music/Farming Music";
                break;
            case (GameManager.GameStateType.ActionPhase):
                eventString = "event:/Music/Battle Music";
                break;
            case (GameManager.GameStateType.GameOver):
                eventString = "event:/Music/Game Over";
                break;
        }
        Music = FMODUnity.RuntimeManager.CreateInstance(eventString);
        Music.start();
    }


    public void StopMusic() {
        if(Music.isValid()) {
            Music.release();
            Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    

}
