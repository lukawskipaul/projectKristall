using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;

    [HideInInspector]
    public bool correctLever = false;

    [SerializeField]
    ParticleSystem[] torchLights;

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
                torchLights[0].Play();
                
                
            }
            else if(i == 2)
            {
                torchLights[1].Play();
                torchLights[2].Play();
            }
            if (i == 3 || CheckPuzzle())
            {
                torchLights[3].Play();
                torchLights[4].Play();
                torchLights[5].Play();

            }
        }
        else
        {
            i = 0;
            correctLever = false;
            for(int i = 0; i < torchLights.Length; i++)
            {
                torchLights[i].Stop();
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
            //torchLights[5].Play();
            dialogue.dialogueLines = SecondSentences;
            dialogue.ItemInteraction();
            creature.SetActive(false);
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.pushBlock);
            return true;
        }
        return false;
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
