using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class MainMenuButton : MonoBehaviour
    {
        public void GoToMainMenu() {
            AudioManager.Instance.Click();
            GameManager.Instance.SetGameState(GameManager.GameStateType.MainMenu);
        }
    }
}
