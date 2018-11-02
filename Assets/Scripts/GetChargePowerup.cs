using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChargePowerup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.pushBlock);
        Debug.Log("PushBlock Activated");
        Destroy(this);
    }
}
