using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public Transform shooterCenter;
    public float triggerDistance = 2f;
    public float timeToStart = 3f;

    private bool starting = false;

    private SpriteRenderer circleSprite;

    void Awake() { 
        GameManager.OnBuildPhaseStart += StartProximityCheck;
        circleSprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        GameManager.OnBuildPhaseStart += StartProximityCheck;
    }

    void OnDisable() {
        GameManager.OnBuildPhaseStart -= StartProximityCheck;
    }

    void StartProximityCheck() {
        Debug.Log("STARTING CHECK");
        StartCoroutine(CheckProximity());
    }

    IEnumerator CheckProximity() {
        float percentStarted = 0;
        while(GameManager.Instance.GameState == GameManager.GameStateType.BuildPhase) {
            if(GameManager.Instance.BuildingManager.GetNumBuildingsInInventory() > 0) {
                percentStarted = 0;
                yield return new WaitForSeconds(0.2f);
                continue;
            }
            //set hole center
            Vector2 distance = (Vector2)(shooterCenter.position - GameManager.Instance.playerFarmer.transform.position);
            transform.position = GameManager.Instance.playerFarmer.transform.position + (Vector3)distance/2;

            //check distance
            if(distance.magnitude <= triggerDistance) {
                starting = true;
            } else if(distance.magnitude > triggerDistance) {
                starting = false;
            }

            //set percent
            if(starting) {
                percentStarted += Time.deltaTime/timeToStart;
            } else {
                percentStarted -= Time.deltaTime/timeToStart;
            }

            circleSprite.color = new Color(circleSprite.color.r, circleSprite.color.g, circleSprite.color.b, percentStarted);
            circleSprite.transform.localScale = new Vector3((1 - percentStarted) * 3.5f, (1 - percentStarted) * 3.5f, 1f);
            
            if(percentStarted < 0) {
                percentStarted = 0;
            } else if (percentStarted >= 1) {
                break;
            }
            yield return null;
        }

        //ending anim
        float timeElapsed = 0;
        float totalTime = 1.5f;
        float waveStartTime = 1f;
        bool waveStarted = false;
        while(timeElapsed < totalTime) {
            if(!waveStarted && timeElapsed < waveStartTime) {
                GameManager.Instance.SetGameState(GameManager.GameStateType.ActionPhase);
            }
            circleSprite.color = new Color(circleSprite.color.r, circleSprite.color.g, circleSprite.color.b, (totalTime - timeElapsed)/totalTime);
            circleSprite.transform.localScale = new Vector3((timeElapsed/totalTime) * 60, (timeElapsed/totalTime) * 60f, 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        circleSprite.color = new Color(circleSprite.color.r, circleSprite.color.g, circleSprite.color.b, 0);
    }
}
