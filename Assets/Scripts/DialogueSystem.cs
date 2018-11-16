using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public Text dialogueText;

    public GameObject dialogBox;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public string[] dialogueLines;

    public bool letterIsMulitplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    

    public string InteractButton;

    public float totalwaitTime;
    private float currentTime;
    private float thisCurrentTime;
    private int currentDialogueIndex;
    private bool stringFinished = false;
    private int dialogueLength;
	// Use this for initialization
	void Start ()
    {
        dialogueText.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ItemInteraction()
    {
        outOfRange = false;
        dialogBox.SetActive(true);
        //Can include a required button to be pressed to interact
        if(!dialogueActive)
        {
            dialogueActive = true;
            StartCoroutine(StartDialogue());
        }
        //StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if(outOfRange == false)
        {
            dialogueLength = dialogueLines.Length;
            currentDialogueIndex = 0;

            while ((currentDialogueIndex < dialogueLength || !letterIsMulitplied) && !dialogueEnded)
            {
                if(!letterIsMulitplied)
                {
                    letterIsMulitplied = true;
                    currentDialogueIndex++;
                    thisCurrentTime = 0;
                    stringFinished = false;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex-1]));
                    
                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                    

                }
                yield return 0;

            }
            
            if (dialogueEnded && stringFinished)
            {
                dialogueEnded = true;
                thisCurrentTime = 0;
                Debug.Log("We got here");
                dialogueEnded = false;
                dialogueActive = false;
                DropDialogue();
            }

        }

        
    }
    private IEnumerator DisplayString(string stringToDisplay)
    {
        if(outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while(currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;
                if(currentCharacterIndex < stringLength)
                {
                    if(Input.GetButtonDown(InteractButton))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    stringFinished = true;
                    break;
                }
            }
            Debug.Log("Premature continuation");
            while(currentTime < totalwaitTime && stringFinished)
            {
                //Debug.Log(currentTime + " Display Strings");
                currentTime+= Time.deltaTime;
                /*
                if (currentTime == totalwaitTime Input.GetButtonDown(InteractButton))
                {
                    currentTime = 0;
                    break;
                }
                */
                yield return 0;

            }
            currentTime = 0;
            dialogueEnded = false;
            letterIsMulitplied = false;
            //currentDialogueIndex++;
            dialogueText.text = "";
            if(currentDialogueIndex >= dialogueLength)
            {
                DropDialogue();
            }
        }
    }

    public void DropDialogue()
    {
        Debug.Log("Dialogue box gone");
        dialogueEnded = true;
        thisCurrentTime = 0;
        Debug.Log("We got here");
        dialogueEnded = false;
        dialogueActive = false;
        dialogBox.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if(outOfRange == true)
        {
            letterIsMulitplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogBox.SetActive(false);
        }
    }
}
