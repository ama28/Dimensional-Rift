using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class PlayButton : MonoBehaviour
    {
        public void Play() {
            GameManager.Instance.StartGame();
        }
    }
}