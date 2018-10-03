using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour, IPowerUp
{

    [SerializeField]
    Rigidbody rigidbody;

    private bool isActivePower;
    private bool isUnlocked;

    public string PowerName
    {
        get
        {
            return "Push";
        }
    }

    public bool IsActivated
    {
        get
        {
            return isActivePower;
        }

        set
        {
            isActivePower = value;
        }
    }

    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }

        set
        {
            isUnlocked = value;
        }
    }

    private void Start()
    {
        //rigidbody.isKinematic = true;
    }

    //Check to see if player is pushing object 
    //check if player has powerup to push object
    //if both are true, player can move object

    //TODO: give player powerup
    //TODO: make the player be sprinting
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Moveable")
        {
            //if(Player.Powerups.Heavyblock && Player.isSprinting)
            // canPush = true;
            //checks to see if player has the powerup and is sprinting (static variables)
            //canPush = true; //HACK
            if (isActivePower && isUnlocked)
            {
                collision.rigidbody.isKinematic = false;
            }
        }
    }

    // reset after pushing is done
    //this will need to be changed so that the object falls normally instead of just stops
    private void OnCollisionExit(Collision collision)
    {
        //GameManager.Instance.canPush = false;
        collision.rigidbody.isKinematic = true;
    }

    
}

