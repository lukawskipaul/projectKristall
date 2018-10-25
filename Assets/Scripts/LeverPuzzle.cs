using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;

    [HideInInspector]
    public bool correctLever = false;

    int i = 0;

    public void CheckLever(GameObject pressedLever)
    {
        GameObject currentLever = pressedLever;

        if (currentLever == leverOrder[i])
        {
            Debug.Log("Correct lever pulled");
            i++;
            correctLever = true;
            if (i == leverOrder.Length)
            {
                //What we want to happen when the puzzle is solved goes here
                Debug.Log("Correct Order!");
                PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.pushBlock);
            }
        }
        else
        {
            i = 0;
            correctLever = false;
        }
    }

}
