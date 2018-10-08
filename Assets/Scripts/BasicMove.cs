using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    private float rotX;
    private float rotY;
    private float rotZ;
    private float VertRotMin = 0f;
    private float VertRotMax = 65f;
    private float mouseX;
    private float mouseY;
    private new Rigidbody rigidbody;

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
        if(rotY <= VertRotMax && rotY >= VertRotMin)
            GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        
    }
}
