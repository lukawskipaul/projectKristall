using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour {

    //Singleton Powerup Manager
    private static TestManager instance;
    public static TestManager Instance { get { return instance; } }

    [SerializeField]
    GameObject Player;

    public TestPush pushBlock;


    List<PowerUp> powerUpsList = new List<PowerUp>();

    PowerUp currentPower;

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
        pushBlock = Player.GetComponent<TestPush>();
        powerUpsList.Add(pushBlock);
    }

    private void Update()
    {
        SelectPowerup();
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
    }

    public void UnlockPowerup(PowerUp powerUp)
    {
        powerUp.IsUnlocked = true;
    }

}
