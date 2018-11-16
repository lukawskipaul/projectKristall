using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform MainMenuTransform;
    public Transform CreditsTransform;
    private Transform currentTransform;
    public float TransitionSpeed;
    public static bool creditsClicked;

	// Update is called once per frame
	void Update ()
    {
		if(creditsClicked)
        {
            currentTransform = CreditsTransform;
        }
        else
        {
            currentTransform = MainMenuTransform;
        }
	}

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentTransform.position, Time.deltaTime * TransitionSpeed);
        Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentTransform.transform.rotation.eulerAngles.x, Time.deltaTime * TransitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentTransform.transform.rotation.eulerAngles.y, Time.deltaTime * TransitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentTransform.transform.rotation.eulerAngles.z, Time.deltaTime * TransitionSpeed));

        transform.eulerAngles = currentAngle;
    }
}
