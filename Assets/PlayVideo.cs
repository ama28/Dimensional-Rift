using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += Play;
    }
    public void Play(VideoPlayer vp)
    {
        AudioManager.Instance.StartCutsceneMusic();
        GameManager.Instance.StartGame();
    }

}
