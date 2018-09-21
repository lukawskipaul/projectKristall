using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour {

    IActivatable activatableObject;

    private void Update()
    {
        HandleInput();
    }

    //We can also do a raycast
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ActivatableObject")
        {
            activatableObject = other.gameObject.GetComponent<IActivatable>();           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        activatableObject = null;
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Activate"))
        {
            if (activatableObject != null)
            {
                activatableObject.DoActivate();
            }
        }
    }
}
