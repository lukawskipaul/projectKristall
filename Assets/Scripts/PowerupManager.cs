﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

    //Singleton Powerup Manager
    private static PowerupManager instance;
    public static PowerupManager Instance { get { return instance; } }

    [SerializeField]
    GameObject Player;

    public PushBlock pushBlock;


    List<IPowerUp> powerUps = new List<IPowerUp>();

    IPowerUp currentPower;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        pushBlock = Player.GetComponent<PushBlock>();
        powerUps.Add(pushBlock);
    }

    private void Update()
    {
        SelectPowerup();
    }

    public void ActivatePower(IPowerUp powerUp)
    {

        foreach (IPowerUp pwrUp in powerUps)
        {
            //Deactivate all other powerups
            if (pwrUp != powerUp)
            {
                pwrUp.IsActivated = false;
            }
        }

        //Activate selected powerup if unlocked
        if (powerUp.IsUnlocked)
        {
            powerUp.IsActivated = true;
            Debug.Log(powerUp.PowerName + " activated");
        }
        else if(!powerUp.IsUnlocked)
        {
            Debug.Log(powerUp.PowerName + " is not unlocked");
        }
        
        
        currentPower = powerUp;
    }

    public void SelectPowerup()
    {
        if (Input.GetButtonDown("Powerup1"))
        {
            ActivatePower(pushBlock);
            
        }
    }

    public void UnlockPowerup(IPowerUp powerUp)
    {
        powerUp.IsUnlocked = true;
    }

}
