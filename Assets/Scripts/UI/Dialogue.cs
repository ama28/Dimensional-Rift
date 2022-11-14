using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public List<TextAsset> dialogueData; //original txt file s
    public float scrollRate = 0.1f; //character is added to string every scrollRate seconds
    public float pauseAfterLine = 2f; //Time break after each line
    public TMP_Text dialogue; //text mesh pro gameobject
    private string dialogueText; //string in textobject
    public int roundsPerAffectionLevel = 7; //test string
    private List<List<List<string>>> parsedOutput = new List<List<List<string>>>(); // parsed list of strings from txt files
    private Queue<string> currentLines; //queue that ChooseDialogue() uses to enqueue the lines of dialogue 
    private bool currentLineEnd; //flag to indicate end of current line

    void OnEnable() {
        GameManager.OnBuildPhaseStart += ChooseDialogue; 
    }

    void OnDisable() {
        GameManager.OnBuildPhaseStart -= ChooseDialogue; 
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLines = new Queue<string>();
        currentLineEnd = false;
        dialogue.SetText(dialogueText);
        for(int i = 0; i < 5; i++) {
            parsedOutput.Add(parseText(dialogueData[i]));
        }
        // ChooseDialogue();
    }

    void Update()
    {
        while (currentLines.Count > 0 && currentLineEnd)
        {
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
                // line = line.Remove(0, 4);
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
        //TODO: Add special case for affection 0

        int affectionLevel = (GameManager.Instance.Level / roundsPerAffectionLevel) + 1;
        List<string> randomDialogue = parsedOutput[affectionLevel][UnityEngine.Random.Range(0, parsedOutput[affectionLevel].Count)];
        foreach (string line in randomDialogue)
        {
            currentLines.Enqueue(line);
        }

        displayNextLine();

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
        Debug.Log(currentLine);
        StopAllCoroutines();
        StartCoroutine(displayText(currentLine));
        
    }

    //Coroutine that concatenates a character from the current dialogue line to the actual text being displayed
    IEnumerator displayText(string currentText)
    {
        yield return new WaitForSeconds(4.0f);
        currentLineEnd = false;
        resetText();
        for (int i = 0; i < currentText.Length; i++)
        {
            //TODO: add whole text displaying stuff or make new class for it
            dialogueText = string.Concat(dialogueText, currentText[i]);
            dialogue.SetText(dialogueText.Substring(4));
            Debug.Log(dialogueText);
            yield return new WaitForSeconds(scrollRate);
        }
        yield return new WaitForSeconds(pauseAfterLine);
        resetText();
        currentLineEnd = true;
    }

    //changes the displayed string to the empty string
    void resetText()
    {
        dialogueText = "";
        dialogue.SetText(dialogueText);
    }
}