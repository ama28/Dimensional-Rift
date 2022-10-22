using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PauseMenu : MonoBehaviour
    {
        Canvas canvas;

        void Start()
        {
            canvas = GetComponent<Canvas>();
        }
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) //todo: change to new input system
            { 
                SetPaused(!canvas.enabled);
            }
        }

        void SetPaused(bool paused) 
        {
            canvas.enabled = paused;
            //todo: actually pause stuff
        }
    }
}
