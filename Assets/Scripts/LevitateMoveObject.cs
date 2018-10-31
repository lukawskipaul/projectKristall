using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateMoveObject : PowerUp {
    public static event Action TeleMovingObject;
    public static event Action TeleStoppedMovingObject;

    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    bool isLevitatingObject = false;

    [SerializeField]
    Transform levitateTransform;

    [SerializeField]
    float levitateFollowSpeed = 3f;

    [SerializeField]
    float transfromMoveSpeed = 3f;

    [SerializeField]
    private float teleEnergy = 100f;

    [SerializeField]
    private float energyDrainRate = 1f;

    [SerializeField]
    private float energyRechargeRate = 10f;

    [SerializeField]
    GameObject player;

    private float xInput;
    private float yInput;
    private float zInput;
    private float maxDist = 5f;
    
    private Vector3 levDirection;
    private Vector3 startingTransform;

    public override string PowerName
    {
        get
        {
            return "Levitate Object";
        }
    }

    private void Start()
    {
        startingTransform = levitateTransform.localPosition;
    }

    private void Update()
    {

        if (isLevitatingObject == true)
        {
            if (Vector3.Distance(player.transform.position, levitateTransform.position) > maxDist || teleEnergy <= 0)
            {
                DropObject(levitatableObj);
                if (teleEnergy < 0)
                {
                    teleEnergy = 0;
                }
            }
            else
            {
                LevitateObject(levitatableObj);
            }

        }
        else if (isLevitatingObject == false)
        {
            teleEnergy += (energyRechargeRate * Time.deltaTime);
        }
        teleEnergy = Mathf.Clamp(teleEnergy, 0, 100);
        Debug.Log("TeleEnergy: " + teleEnergy);
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        GetObjectRigidBody(objectToLevitate);
        objectRigidBody.useGravity = false;
        objectToLevitate.layer = 9;
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        OnTeleMovingObject();
        MoveLevitateTransform();
        MoveLevitateObject(objectToLevitate, objectTransfrom);
        teleEnergy -= (energyDrainRate * Time.deltaTime);
        Debug.Log("LevitatingObj");
    }

    private void GetObjectRigidBody(GameObject objToLevitate)
    {
        try
        {
            objectRigidBody = objToLevitate.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            Debug.Log("No rigidbody");
            throw;
        }
    }

    private void MoveLevitateObject(GameObject objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.transform.position = objTransform;
    }

    private void MoveLevitateTransform()
    {
        Transform teleTransform = gameObject.transform;
        xInput = Input.GetAxis("Mouse X");
        yInput = Input.GetAxis("Mouse Y");
        zInput = Input.GetAxis("levZ");

        levDirection = new Vector3(xInput, yInput, zInput);

        levitateTransform.Translate(levDirection * transfromMoveSpeed * Time.deltaTime);
    }

    private void DropObject(GameObject objectToDrop)
    {
        try
        {
            objectRigidBody = objectToDrop.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            Debug.Log("No rigidbody");
            throw;
        }
        objectToDrop.layer = 0;
        isLevitatingObject = false;
        objectRigidBody.useGravity = true;
        ResetLevTransform();
        OnTeleStoppedMovingObject();
    }

    public override void UsePower(GameObject objToLevitate)
    {
        if (objToLevitate != null)
        {
            if (objToLevitate.tag == "LevitatableObject")
            {
                if (isLevitatingObject)
                {
                    DropObject(objToLevitate);
                }
                else if (!isLevitatingObject)
                {
                    isLevitatingObject = true;
                }

            }
        }
        else
        {
            Debug.Log("No levitatable object");
        }

    }

    private void SetLevitatableObject(GameObject gameObject)
    {
        if (!isLevitatingObject)
        {
            levitatableObj = gameObject;
            Debug.Log(levitatableObj.name + " can be levitated.");
        }

    }

    private void RemoveLevitatableObj(GameObject gameObject)
    {
        if (isLevitatingObject)
        {
            DropObject(levitatableObj);
        }
        levitatableObj = null;
    }

    private void ResetLevTransform()
    {
        levitateTransform.localPosition = startingTransform;
    }

    private void OnTeleMovingObject()
    {
        if (TeleMovingObject != null)
        {
            TeleMovingObject.Invoke();
        }
    }

    private void OnTeleStoppedMovingObject()
    {
        if (TeleStoppedMovingObject != null)
        {
            TeleStoppedMovingObject.Invoke();
        }
    }

    private void OnEnable()
    {
        DetectObject.LevObjectDetected += SetLevitatableObject;
        //DetectObject.LevObjectExit += ResetLevitatableObj;
    }

    private void OnDisable()
    {
        DetectObject.LevObjectDetected -= SetLevitatableObject;
        //DetectObject.LevObjectExit += ResetLevitatableObj;
    }
}
