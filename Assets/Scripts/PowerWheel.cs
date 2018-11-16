﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerWheel : MonoBehaviour {

    //public Text instructions;
    public GameObject powerWheel, rotationText;
    private bool activated = false;
    PowerupManager powerupManager;

    [TextArea(5, 10)]
    public string[] instructions;
    public Text description;

	// Use this for initialization
	void Start ()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("TogglePowerWheel") && activated == false)
        {
            powerWheel.SetActive(true);
            activated = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetButtonDown("TogglePowerWheel") && activated == true)
        {
            powerWheel.SetActive(false);
            activated = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}

    public void OnChargeHover()
    {
        if(powerupManager.pushBlock.IsUnlocked == true)
        {
            powerupManager.ActivatePower(powerupManager.pushBlock);
            description.text = instructions[0];
            rotationText.SetActive(false);
        }
        else
        {
            description.text = instructions[2];
        }
        
    }

    public void OnTelekinesisHover()
    {
        if(powerupManager.levitateMoveObject.IsUnlocked == true)
        {
            powerupManager.ActivatePower(powerupManager.levitateMoveObject);
            description.text = instructions[1];
            rotationText.SetActive(true);
        }
        else
        {
            description.text = instructions[3];
        }
        
    }
}
