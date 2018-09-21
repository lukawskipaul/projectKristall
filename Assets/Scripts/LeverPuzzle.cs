﻿using System.Collections;
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
            Debug.Log("Lever pulled");
            i++;

            if (i == leverOrder.Length)
            {
                Debug.Log("Correct Order!");
                
            }
        }
        else
        {
            i = 0;
        }
    }

}
