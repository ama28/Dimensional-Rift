using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseIcon : MonoBehaviour
{
    public enum iconType { build, enemy }
    [SerializeField] private iconType type; 

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStateType.BuildPhase)
        {
            if (type == iconType.build)
                gameObject.GetComponent<Image>().enabled = true;
            else if (type == iconType.enemy)
                gameObject.GetComponent<Image>().enabled = false;
        }
        else if (GameManager.Instance.GameState == GameManager.GameStateType.ActionPhase)
        {
            if (type == iconType.build)
                gameObject.GetComponent<Image>().enabled = false;
            else if (type == iconType.enemy)
                gameObject.GetComponent<Image>().enabled = true;
        }
        
    }
}
