using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace UI {
    public class PlayButton : MonoBehaviour
    {
        public GameObject skipButton;
        public GameObject controlsDisplay;
        public GameObject videoPlayer;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeControls();
            }
        }

        public void playVideo()
        {
            videoPlayer.SetActive(true);
            AudioManager.Instance.StartCutsceneMusic();
            AudioManager.Instance.Click();
            print("playing video" + videoPlayer);
            /*videoPlayer = GetComponent<VideoPlayer>();*/
            videoPlayer.GetComponent<VideoPlayer>().Play();
            skipButton.SetActive(true);
        }

        public void skipVideo()
        {
            GameManager.Instance.StartGame();
        }

        public void openControls()
        {
            controlsDisplay.SetActive(true);
        }

        private void closeControls()
        {
            controlsDisplay.SetActive(false);
        }
    }
}
