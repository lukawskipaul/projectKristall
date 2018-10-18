using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : PowerUp
{
    public override string PowerName
    {
        get
        {
            return "Push";
        }
    }

    //Check to see if object is Moveable
    //check if player has powerup to push object
    //if both are true, player can move object

    //TODO: make the player be sprinting
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Moveable")
        {

            if (IsActivated && IsUnlocked)
            {
                collision.rigidbody.isKinematic = false;
            }
        }
    }

    // reset after pushing is done
    //this will need to be changed so that the object falls normally instead of just stops
    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.tag == "Moveable")
        {
            collision.rigidbody.isKinematic = true;
        }
    }


}

