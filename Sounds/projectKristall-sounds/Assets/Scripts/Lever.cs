using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IActivatable {

    [SerializeField]
    string nameText;

    [SerializeField]
    LeverPuzzle leverPuzzle;

    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    public void DoActivate()
    {
        // whatever we want to happen
        leverPuzzle.CheckLever(this.gameObject);


        Debug.Log("Lever Activated");  // just for testing
    }
}
