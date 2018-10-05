using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public float rotX;
    public float rotY;
    public float rotZ;
    public Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        
        rigidbody.detectCollisions = true;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            
           rigidbody.transform.position += rigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            rigidbody.transform.position += rigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            rigidbody.transform.position -= rigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed; 
        }

        if(Input.GetKey("a") && !Input.GetKey("d"))
        {
            rigidbody.transform.position += rigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            rigidbody.transform.position -= rigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        

        /*
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        */

        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
        {
            if (rigidbody.transform.position.y <= 1.05f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
            }
        }
        else if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (rigidbody.transform.position.y <= 1.05f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
            }
        }
        
    }

    private void Update()
    {
        
        rigidbody.detectCollisions = true;
        rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        
    }
}
