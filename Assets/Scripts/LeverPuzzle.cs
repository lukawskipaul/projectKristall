using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;


    int i = 0;

    public void CheckLever(GameObject pressedLever)
    {
        GameObject currentLever = pressedLever;

        if (currentLever == leverOrder[i])
        {
            Debug.Log("Correct lever pulled");
            i++;

            if (i == leverOrder.Length)
            {
                //What we want to happen when the puzzle is solved goes here
                Debug.Log("Correct Order!");
                TestManager.Instance.UnlockPowerup(TestManager.Instance.pushBlock);
            }
        }
        else
        {
            i = 0;
        }
    }

}
