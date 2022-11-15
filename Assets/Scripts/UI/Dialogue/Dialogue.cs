using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextAsset dialogueData; //original txt file 
    public float scrollRate; //character is added to string every scrollRate seconds
    public TMP_Text dialogue; //text mesh pro gameobject
    public TMP_Text speaker; //text mesh pro gameobject for name of speaker character 
    private string dialogueText; //string in textobject
    public string test; //test string
    private List<List<string>> parsedOutput; // parsed list of strings from txt file
    private Queue<string> currentLines; //queue that ChooseDialogue() uses to enqueue the lines of dialogue 
    private bool currentLineEnd; //flag to indicate end of current line
    public TMP_FontAsset satella;
    public TMP_FontAsset vt323;
    public int satella_size;
    public int vt323_size;
    public GameObject samDialogueBox;
    public GameObject fridaDialogueBox;

    private string currentSpeaker;
    // Start is called before the first frame update
    void Start()
    {
        currentLines = new Queue<string>();
        currentLineEnd = false;
        dialogue.SetText(dialogueText);
        parsedOutput = parseText(dialogueData);
       // samDialogueBox.SetActive(false);
       // fridaDialogueBox.SetActive(false);
        ChooseDialogue();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        //while (currentLines.Count > 0 && currentLineEnd)
        {
            if (currentLines.Count <= 0)
            {
                resetText();
                resetSpeaker();
            }
            displayNextLine();
        }

        if (Input.GetKeyDown(KeyCode.U))

        {
            ChooseDialogue();
        }
    }
    //parses the txt file into a list of strings
    private List<List<string>> parseText(TextAsset input)
    {
        List<List<string>> result = new List<List<string>>();
        string rawText = input.text;
        string[] lines = rawText.Split("\n");
        int linesLength = lines.Length;
        List<string> currentDialogueExchange = new List<string>();
        for (int i = 0; i < linesLength; i++)
        {
            string line = lines[i];
            if (!(line.StartsWith("-")))
            {
                //line = line.Remove(0, 4);
                currentDialogueExchange.Add(line);
            }

            else
            {
                result.Add(currentDialogueExchange);
                currentDialogueExchange = new List<string>();
            }
        }
        return result;

    }

    //chooses a random dialogue from the parsed list of lists
    public void ChooseDialogue()
    {
        currentLines.Clear();
        List<string> randomDialogue = parsedOutput[UnityEngine.Random.Range(0, parsedOutput.Count)];
        foreach (string line in randomDialogue)
        {
            currentLines.Enqueue(line);
        }

        displayNextLine();

    }
    //sets assets and font in accordance to speaker character
    public void setSpeaker(string speakerCode, string currentLine)
    {
        switch (speakerCode)
        {
            case "S::":
                currentSpeaker = "sam";
                speaker.SetText("Sam");
                dialogue.font = satella;
                speaker.font = satella;
                speaker.fontSize = satella_size +3;
                dialogue.fontSize = satella_size;

                samDialogueBox.SetActive(true);
                fridaDialogueBox.SetActive(false);
                print("what why");
                break;
            case "F::":
                speaker.SetText("Frida");
                dialogue.font = vt323;
                speaker.font = vt323;
                speaker.fontSize = vt323_size;
                dialogue.fontSize = vt323_size;

                samDialogueBox.SetActive(false);
                fridaDialogueBox.SetActive(true);
                break;
            default:
                break;
        }

    }

    //displayNextLine attempts to dequeue from currentLines and calls the displayText Coroutine if it succeeds
    public void displayNextLine()
    {
        if (currentLines.Count == 0)
        {
            //no more lines left to display
            return;
        }

        string currentLine = currentLines.Dequeue();
        if (currentLine.Substring(0,3).Contains("::"))
        {
            setSpeaker(currentLine.Substring(0, 3), currentLine);
            currentLine = currentLine.Remove(0, 4);
        }
        //Debug.Log(currentLine);
        StopAllCoroutines();
        StartCoroutine(displayText(currentLine));
        
    }

    //Coroutine that concatenates a character from the current dialogue line to the actual text being displayed
    IEnumerator displayText(string currentText)
    {
        print("start new line?????");
        currentLineEnd = false;
        resetText();
        for (int i = 0; i < currentText.Length; i++)
        {
            dialogueText = string.Concat(dialogueText, currentText[i]);
            print("dialogue" + dialogue.font);
            dialogue.SetText(dialogueText);
           // Debug.Log(dialogueText);
            yield return new WaitForSeconds(scrollRate);
        }
        currentLineEnd = true;
    }

    //changes the displayed string to the empty string
    void resetText()
    {
        print("called rando");
        dialogueText = "";
        dialogue.SetText(dialogueText);
        samDialogueBox.SetActive(false);
        fridaDialogueBox.SetActive(false);

    }
    void resetSpeaker()
    {
        speaker.SetText("");
       // samDialogueBox.SetActive(false);
        //fridaDialogueBox.SetActive(false);
        //dialogue.SetText(dialogueText);
    }
}