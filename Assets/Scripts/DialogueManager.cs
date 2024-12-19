using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System.Data;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    //[SerializeField] private float typingSpeed = 1f;
    private bool CanContinueToNextLine = false;

    [Header("DialogueUI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private bool activechoices = false;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private Coroutine DisplayLineCoroutine;


    // Enter or selecting options
    // Space to skip dialogue
    // Esc to close dialogue box

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitDialogueMode());
        }

        if (CanContinueToNextLine) //&& Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.05f); // Changes the typing speed
        //Debug.Log("Exiting Dialogue mode");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (DisplayLineCoroutine != null)
            {
                StopCoroutine(DisplayLineCoroutine);
            }
            DisplayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        }
        else
        {
            if (!activechoices)
            {
            StartCoroutine(ExitDialogueMode());
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

                            // defensive check to make sure our UI can support the number of choices coming in
                            if (currentChoices.Count > choices.Length)
                            {
                                Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                                    + currentChoices.Count);
                            }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            //Debug.Log("Choice on screen"); //Shows the choices on the screen
            activechoices = true;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
       // Debug.Log("Waitforselection firstselectchoice true");
    }
    public void MakeChoice(int choiceIndex)
    {
        if(CanContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        HideChoices();
        CanContinueToNextLine = false;

        bool IsAddingRichTextTag = false;
        
        foreach (char letter in line.ToCharArray())
        { 
                                    if (Input.GetKeyDown(KeyCode.Space))
                                   {
                                       dialogueText.text = line;
                                        break;
                                    }

        if (letter == '<' || IsAddingRichTextTag)
            {
                IsAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>') 
                {
                    IsAddingRichTextTag = false;
                }
            }
        else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.015f); // Changes the typing speed
            }
        }
        yield return new WaitForSeconds(0.5f); //Waits a period before next line
        //Debug.Log("WAITED 0.8 SECS");
        DisplayChoices();
        CanContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
}
