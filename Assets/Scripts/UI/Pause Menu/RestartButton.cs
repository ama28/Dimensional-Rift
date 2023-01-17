using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class RestartButton : MonoBehaviour
    {
        public void OnClick() {
            AudioManager.Instance.Click();
            GameManager.Instance.Restart();
        }
    }
}
