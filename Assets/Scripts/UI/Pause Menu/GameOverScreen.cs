using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class GameOverScreen : MonoBehaviour
    {
        Canvas canvas;
        [SerializeField] TextMeshProUGUI text;

        void Start()
        {
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        void OnEnable() {
            GameManager.OnGameOver += GameOver;
        }

        void OnDisable() {
            GameManager.OnGameOver -= GameOver;
        }

        void GameOver() {
            text.text = "Final Score: " + GameManager.Instance.Level;
            canvas.enabled = true;
            Time.timeScale = 0f;
        }
    }
}
