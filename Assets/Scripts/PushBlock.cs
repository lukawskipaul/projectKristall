using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    bool canPush;
    [SerializeField]
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody.isKinematic = true;
    }

    //Check to see if player is pushing object 
    //check if player has powerup to push object
    //if both are true, player can move object

    //TODO: give player powerup
    //TODO: make the player be sprinting
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            //if(Player.Powerups.Heavyblock && Player.isSprinting)
            // canPush = true;
            //checks to see if player has the powerup and is sprinting (static variables)
            canPush = true; //HACK
            if(canPush)
            {
                rigidbody.isKinematic = false;
            }
        }
    }

    // reset after pushing is done
    //this will need to be changed so that the object falls normally instead of just stops
    private void OnCollisionExit(Collision collision)
    {
        canPush = false;
        rigidbody.isKinematic = true;
    }
}
