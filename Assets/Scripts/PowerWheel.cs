using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerWheel : MonoBehaviour {

    //public Text instructions;
    public GameObject powerWheel;
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
        }
        else if(Input.GetButtonDown("TogglePowerWheel") && activated == true)
        {
            powerWheel.SetActive(false);
        }
	}

    public void OnChargeHover()
    {
        if(powerupManager.pushBlock.IsActivated == true)
        {
            powerupManager.ActivatePower(powerupManager.pushBlock);
            description.text = instructions[0];
        }
        else
        {
            description.text = instructions[2];
        }
        
    }

    public void OnTelekinesisHover()
    {
        if(powerupManager.levitateMoveObject.IsActivated == true)
        {
            powerupManager.ActivatePower(powerupManager.levitateMoveObject);
            description.text = instructions[1];
        }
        else
        {
            description.text = instructions[3];
        }
        
    }
}
