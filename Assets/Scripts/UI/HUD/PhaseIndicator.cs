using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class PhaseIndicator : MonoBehaviour
    {
        TextMeshProUGUI text;
        void Awake() {
            text = GetComponent<TextMeshProUGUI>();
        }

        void OnWaveStart(Wave wave) {
            text.text = "Wave " + GameManager.Instance.Level;
        }

        void OnBuildStart() {
            text.text = "Build Phase";
        }

        void OnEnable() {
            GameManager.OnActionPhaseStart += OnWaveStart;
            GameManager.OnBuildPhaseStart += OnBuildStart;
        }

        void OnDisable() {
            GameManager.OnActionPhaseStart -= OnWaveStart;
            GameManager.OnBuildPhaseStart -= OnBuildStart;
        }


    }
}
