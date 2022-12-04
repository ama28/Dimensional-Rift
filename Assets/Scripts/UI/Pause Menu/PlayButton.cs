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
            print("playing video" + videoPlayer);
            /*videoPlayer = GetComponent<VideoPlayer>();*/
            videoPlayer.GetComponent<VideoPlayer>().Play();
        }
        public void Play() {
            AudioManager.Instance.Click();
            GameManager.Instance.StartGame();
        }
    }
}
