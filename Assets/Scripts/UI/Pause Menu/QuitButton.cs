using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class QuitButton : MonoBehaviour
    {
        public void QuitGame() {
            AudioManager.Instance.Click();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
