using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;
    public float maxDistance;
    private float currentDistance;

    public static bool fired;
    public static bool hooked;

    private void Update()
    {
        //firing hook
        if (InputManager.BButton() && fired == false)
            fired = true;

        if (fired == true)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
                ReturnHook();
        }
    }

    void ReturnHook()
    {
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}

