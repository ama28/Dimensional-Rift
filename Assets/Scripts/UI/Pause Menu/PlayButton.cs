using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace UI {
    public class PlayButton : MonoBehaviour
    {
        public GameObject videoPlayer;
        public void playVideo()
        {
            videoPlayer.SetActive(true);
            AudioManager.Instance.StartCutsceneMusic();
            AudioManager.Instance.Click();
            print("playing video" + videoPlayer);
            /*videoPlayer = GetComponent<VideoPlayer>();*/
            videoPlayer.GetComponent<VideoPlayer>().Play();
        }
    }
}
