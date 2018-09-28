using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;

    PushBlock pushBlock;


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
                Debug.Log("Correct Order!");

                GameManager.Instance.ActivatePower(pushBlock);
            }
        }
        else
        {
            i = 0;
        }
    }

}
