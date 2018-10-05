using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float movementSpeed;

	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed; 
        }

        if(Input.GetKey("a") && !Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (transform.position.y <= 1.05f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 2000);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (transform.position.y <= 1.05f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
            }
        }
    }
}
