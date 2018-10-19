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
    private bool canMove = true;
    public Rigidbody playerRigidbody;
    private Vector3 vector3;
    

    [SerializeField]
    GameObject camRig;

    [SerializeField]
    CapsuleCollider playerCollider;

    [SerializeField]
    LayerMask groundLayers;

    [SerializeField]
    public float jumpForce = 100f;

    [SerializeField]
    public float superJumpModifier = 2f;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        
	}

    void FixedUpdate()
    {
        Debug.Log("Is on ground:" + IsGrounded());
        if (canMove)
        {
            playerRigidbody.detectCollisions = true;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
            {

                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
            }
            else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey("s"))
            {
                playerRigidbody.transform.position -= playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                playerRigidbody.transform.position -= playerRigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && IsGrounded())
            {
                GetComponent<Rigidbody>().AddForce((Vector3.up * jumpForce), ForceMode.Impulse);
            }
        }

    }

    public void SuperJump()
    {
        if (InputManager.AButton() && Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * (jumpForce * superJumpModifier), ForceMode.Impulse);
        }        
    }

    private void Update()
    {
        if (canMove)
        {
            playerRigidbody.detectCollisions = true;
            rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
            rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

            if (rotX < -35f)
            {
                rotX = -35f;
            }
            else if (rotX > 35f)
            {
                rotX = 35f;
            }

            transform.rotation = Quaternion.Euler(0, rotY, 0);
            //GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
            camRig.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        }
            
    }


    public bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center,
            new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), playerCollider.radius * 0.9f, groundLayers);

        //RaycastHit hit;
        //float distance = 1f;

        //Vector3 dir = new Vector3(0, -1);
        //Debug.DrawRay(transform.position, dir, Color.red);
        //if (Physics.Raycast(transform.position, dir, out hit, distance))
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }

    private void StopMove()
    {
        canMove = false;

    }

    private void StartMove()
    {
        canMove = true;
    }

    private void OnEnable()
    {
        LevitateMoveObject.TeleMovingObject += StopMove;
        LevitateMoveObject.TeleStoppedMovingObject += StartMove;
    }

    private void OnDisable()
    {
        LevitateMoveObject.TeleMovingObject -= StopMove;
        LevitateMoveObject.TeleStoppedMovingObject -= StartMove;
    }
}
