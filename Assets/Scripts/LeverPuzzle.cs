﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;

    [HideInInspector]
    public bool correctLever = false;

    [SerializeField]
    Light[] torchLights;

    [SerializeField]
    GameObject creature;

    int i = 0;
    public string[] FirstSentences;
    public string[] SecondSentences;

    private DialogueSystem dialogue;
    bool isChainDestroyed = false;

    public void Start()
    {
        dialogue = FindObjectOfType<DialogueSystem>();
    }

    public void CheckLever(GameObject pressedLever)
    {
        GameObject currentLever = pressedLever;

        if (currentLever == leverOrder[i])
        {
            Debug.Log("Correct lever pulled");
            i++;
            correctLever = true;
            if(i == 1)
            {
                torchLights[0].enabled = true;
                
                
            }
            else if(i == 2)
            {
                torchLights[1].enabled = true;
                torchLights[2].enabled = true;
            }
            if (CheckPuzzle())
            {

            }
        }
        else
        {
            i = 0;
            correctLever = false;
            for(int i = 0; i < torchLights.Length; i++)
            {
                torchLights[i].enabled = false;
                dialogue.dialogueLines = FirstSentences;
                dialogue.ItemInteraction();
                
            }
        }
    }

    private bool CheckPuzzle()
    {
        if (i == leverOrder.Length && isChainDestroyed)
        {
            //What we want to happen when the puzzle is solved goes here
            Debug.Log("Puzzle Solved and Chain broken!");
            torchLights[6].enabled = true;
            dialogue.dialogueLines = SecondSentences;
            dialogue.ItemInteraction();
            creature.SetActive(false);
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.pushBlock);
            return true;
        }
        return false;
    }

    public void OnTriggerExit(Collider other)
    {
        dialogue.OutOfRange();
        
    }

    private void DestroyChain()
    {
        isChainDestroyed = true;
        Debug.Log("chain Destroyed");
        CheckPuzzle();
    }

    private void OnEnable()
    {
        CrystalShot.ObjectDestroyed += DestroyChain;
    }

    private void OnDisable()
    {
        CrystalShot.ObjectDestroyed -= DestroyChain;
    }

}
