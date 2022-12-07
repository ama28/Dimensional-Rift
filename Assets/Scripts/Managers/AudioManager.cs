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

    void OnDisable() {
        GameManager.OnBuildPhaseStart -= StartMusic;
        GameManager.OnActionPhaseStart -= StartMusic;
        GameManager.OnGameOver -= StartMusic;
        GameManager.OnMainMenu -= StartMusic;
    }

    void Start()
    {
        StartMusic();
    }

    void StartMusic(Wave wave) {
        StartMusic();
    }

    void Update() {
        Debug.Log(GameManager.Instance.GameState);
    }

    public void StartMusic() {
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
                int gameOverChooser = Random.Range(1, 10);
                if (gameOverChooser > 1) {
                    eventString = "event:/Music/Game Over 1";
                } else {
                    eventString = "event:/Music/Game Over 2";
                }
                break;
        }
        Music = FMODUnity.RuntimeManager.CreateInstance(eventString);
        Music.start();
    }

    public void StartCutsceneMusic() {
        Debug.Log("Test2");
        StopMusic();
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Cutscene");
        Music.start();

    }


    public void StopMusic() {
        // if(Music.isValid()) {
            Debug.Log("Test");
            Music.release();
            Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // }
    }

    //Speaking
    public void Speak(string character) {
        switch(character) {
            case("Freida"): {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Voices/Freida");
                break;
            }
            case("Sam"): {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Voices/Sam");
                break;
            }
            default:
                Debug.LogError("invalid character: " + character);
                break;

        }
    }

    //Damage
    public void FarmDamage() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Damage/FarmDamage");
    }

    public void EnemyDamage() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Damage/EnemyDamage");
    }

    public void FreidaDamage() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Damage/FreidaDamage");
    }

    // Item and weapon sounds

    public void FireGun(GunInfo.SFXType sfxType) {
        switch(sfxType) {
            case GunInfo.SFXType.Gunshot:
                Gun();
                break;
            case GunInfo.SFXType.Pistol:
                Pistol();
                break;
            case GunInfo.SFXType.Sniper:
                Sniper();
                break;
            case GunInfo.SFXType.Laser:
                Laser();
                break;
        }
    }
    
    public void Laser() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/laser");
    }

    public void Sniper() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/sniper");
    }
    public void Pistol() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/pistol");
    }
    public void Gun() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/gunshot");
    }

    public void Coin(int coinType) { //done
        if (coinType == 1) {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/coin");
        } else {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/coin 2");
        }
    }
    public void FarmPlace() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/FarmPlace");
    }
    public void FarmHarvest() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Items/FarmHarvest");
    }

    // UI Sounds

    public void ShopClick() { //done
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/shopclick");
    }

    public void Mouseover() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/mouseover");
    }

    public void Click() { //done
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/click");
    }

    public void Back() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/back");
    }

    public void TBD() {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/idk");
    }
}
