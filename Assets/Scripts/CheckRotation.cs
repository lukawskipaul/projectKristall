using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotation : MonoBehaviour {

    public event Action RotationCollision;
    public event Action RotationCollisionExit;

    MeshCollider rotationCheckCollider;
    bool isColliding = false;

	// Use this for initialization
	void Start () {
        rotationCheckCollider = GetComponent<MeshCollider>();
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "LevitatableObj")
        {
            isColliding = true;
        }
        //OnRotationCollision();       

    }

    private void OnTriggerExit(Collider other)
    {
        //OnRotationCollisionExit();
        Debug.Log("Exit");
        isColliding = false;
    }

    public bool WillCollide()
    {
        StartCoroutine(WaitRotationCheck());
        Debug.Log("Collision?: " + isColliding);
        return isColliding;
    }

    IEnumerator WaitRotationCheck()
    {
        yield return new WaitForSeconds(1f);
    }
    //private void OnRotationCollision()
    //{
    //    if (RotationCollision != null)
    //    {
    //        RotationCollision.Invoke();
    //    }        
    //}

    //private void OnRotationCollisionExit()
    //{
    //    if (RotationCollisionExit != null)
    //    {
    //        RotationCollisionExit.Invoke();
    //    }       
    //}
}
