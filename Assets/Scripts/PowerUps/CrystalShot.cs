using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShot : MonoBehaviour
{
    public static event Action ObjectDestroyed;
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "LevitatableObject")
        {
            PowerupManager.Instance.levitateMoveObject.SetLevitatableObject(collision.gameObject);
            
        }
        if(collision.gameObject.tag == "DestructibleObjects")
        {
            Destroy(collision.gameObject);
            OnObjectDestroyed();
        }
        Destroy(this.gameObject);
    }

    private void OnObjectDestroyed()
    {
        if (ObjectDestroyed != null)
        {
            ObjectDestroyed.Invoke();
        }
    }
}
