using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScrollingText : MonoBehaviour
{
    public string startText; //starting text used for testing
    public float scrollRate; //character is added to string every scrollRate seconds
    public TMP_Text dialogue; //text object
    private string dialogueText; //string in textobject
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetText(dialogueText);
        resetText();
        StartCoroutine(displayText());
    }

    IEnumerator displayText()
    {
      for (int i = 0; i < startText.Length; i++) 
      {
        dialogueText = string.Concat(dialogueText, startText[i]);
        dialogue.SetText(dialogueText);
        Debug.Log(dialogueText);
        yield return new WaitForSeconds(scrollRate);
      }
    }

    void resetText()
    {
        dialogueText = "";
        dialogue.SetText(dialogueText);
    }

}
