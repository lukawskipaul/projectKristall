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
    public LevitateObjectPowerUp levitateObject;
    public HoverPowerup hoverPowerup;
    public LevitateMoveObject levitateMoveObject;
    public AirDashPower airDash;
    public SuperJump superJump;

    List<PowerUp> powerUpsList = new List<PowerUp>();

    public PowerUp currentPower;

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
        powerUpsList.Add(pushBlock);
    }

    private void Update()
    {
        SelectPowerup();
        HandleInput();
    }

    public void ActivatePower(PowerUp powerUp)
    {

        foreach (PowerUp pwrUp in powerUpsList)
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
        else if (!powerUp.IsUnlocked)
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
        if (Input.GetButtonDown("Powerup2"))
        {
            UnlockPowerup(levitateObject);
            ActivatePower(levitateObject);

        }
        if (Input.GetButtonDown("Powerup3"))
        {
            UnlockPowerup(hoverPowerup);
            ActivatePower(hoverPowerup);

        }
        if (Input.GetButtonDown("Powerup4"))
        {
            UnlockPowerup(levitateMoveObject);
            ActivatePower(levitateMoveObject);

        }
        if (Input.GetButtonDown("Powerup5"))
        {
            UnlockPowerup(airDash);
            ActivatePower(airDash);

        }
        if (Input.GetButtonDown("Powerup6"))
        {
            UnlockPowerup(superJump);
            ActivatePower(superJump);

        }
    }

    void HandleInput()
    {

        if (Input.GetButtonDown("UsePower"))
        {
            if (currentPower == levitateObject)
            {
                currentPower.UsePower(levitateObject.levitatableObj);
            }
            if (currentPower == hoverPowerup)
            {
                currentPower.UsePower();
            }
            if (currentPower == levitateMoveObject)
            {
                currentPower.UsePower(levitateMoveObject.levitatableObj);
            }
            if (currentPower == airDash)
            {
                currentPower.UsePower();
            }
        }

    }

    public void UnlockPowerup(PowerUp powerUp)
    {
        powerUp.IsUnlocked = true;
    }

    

}
