using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using FMOD;
using FMODUnity;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager i;
    private static FMOD.Studio.EventInstance Music;

    void OnEnable() {
        GameManager.OnBuildPhaseStart += StartMusic;
        GameManager.OnActionPhaseStart += StartMusic;
        GameManager.OnGameOver += StartMusic;
        GameManager.OnMainMenu += StartMusic;
    }

    void Start()
    {
        StartMusic();
    }

    void StartMusic(Wave wave) {
        StartMusic();
    }

    public void StartMusic() {
        Debug.Log("????");
        StopMusic();
        //switch music using game state
        string eventString;
        switch(GameManager.Instance.GameState)
        { 
            case (GameManager.GameStateType.MainMenu):
                eventString = "event:/Music/Main Menu";
                break;
            default:
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

    // Item and weapon sounds
    
    public void Laser() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/laser");
    }

    public void Sniper() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/sniper");
    }

    public void Coin() {
        int coinSoundChooser = Random.Range(0, 2);
        if (coinSoundChoose == 1) {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/coin");
        } else {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/coin2");
        }
    }

    // UI Sounds

    public void ShopClick() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/shopclick");
    }

    public void Mouseover() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/mouseover");
    }

    public void Click() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/click");
    }

    public void Back() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/back");
    }

    public void TBD() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/idk");
    }
}
