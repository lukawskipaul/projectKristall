﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevitateMoveObject : PowerUp
{
    public static event Action TeleMovingObject;
    public static event Action TeleStoppedMovingObject;

    public static event Action<GameObject> SendLevitatingObj;



    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    GameObject levitatingObj;
    bool isLevitatingObject = false;
    bool isRotating = false;
    bool isCarrying = false;
    bool wasLevitating = false;
    bool isPulling = false;
    bool canRotate;

    CheckRotation checkRotation;
    MeshCollider rotationCheckCollider;

    [SerializeField]
    Slider energySlider;

    [SerializeField]
    Transform levitateTransform, MinDistRayTransform;

    [SerializeField]
    float levitateFollowSpeed = 3f;

    [SerializeField]
    float transfromMoveSpeed = 3f, rotationAngleSnap = 15f;

    private float teleEnergy = 100f;

    [SerializeField]
    float maxEnergy = 100f;

    [SerializeField]
    private float energyDrainRate = 1f;

    [SerializeField]
    private float energyRechargeRate = 10f, maxDist = 5f, minDist = 1f;

    [SerializeField]
    GameObject player;

    [SerializeField]
    LayerMask levitatingObjLayer;

    private float xInput;
    private float yInput;
    private float zInput;

    private Vector3 levDirection;
    private Vector3 startingTransform;

    public override string PowerName
    {
        get
        {
            return "Telekinesis";
        }
    }

    private void Start()
    {
        startingTransform = levitateTransform.localPosition;
        energySlider.value = EnergyPercent();
    }

    private void Update()
    {
        //Debug.Log(isPulling);
        if (isLevitatingObject == true)
        {
            if (Vector3.Distance(player.transform.position, levitatingObj.transform.position) > maxDist || teleEnergy <= 0)
            {
                if (isPulling && teleEnergy > 0)
                {
                    MoveLevitateObject(levitatableObj, new Vector3(levitatableObj.transform.position.x, levitatableObj.transform.position.y, levitatableObj.transform.position.z));
                    if (Vector3.Distance(player.transform.position, levitatingObj.transform.position) < maxDist)
                    {
                        isPulling = false;
                    }

                }
                else
                {
                    DropObject(levitatableObj);
                }

            }
            else
            {
                if (Input.GetButtonDown("ToggleAlternateAbility"))
                {
                    if (isRotating)
                    {
                        isRotating = false;
                    }
                    else
                    {
                        isRotating = true;
                        isCarrying = false;
                    }
                }
                if (Input.GetButtonDown("ToggleCarry"))
                {
                    if (isCarrying)
                    {
                        isCarrying = false;
                    }
                    else
                    {
                        isCarrying = true;
                        isRotating = false;
                    }
                }
                LevitateObject(levitatableObj);
                isPulling = false;
            }

        }
        else if (isLevitatingObject == false)
        {
            teleEnergy += (energyRechargeRate * Time.deltaTime);
        }
        teleEnergy = Mathf.Clamp(teleEnergy, 0, maxEnergy);
        energySlider.value = EnergyPercent();
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        if (!wasLevitating)
        {
            GetObjectRigidBody(objectToLevitate);
            objectRigidBody.useGravity = false;
            objectToLevitate.layer = 11;
            //objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
            objectRigidBody.velocity = Vector3.zero;    //Stops the object from moving once you let it go
            objectRigidBody.angularVelocity = Vector3.zero;
            wasLevitating = true;
            //checkRotation = objectToLevitate.GetComponentInChildren<CheckRotation>(); //Each LevitatableObject will have a child that contains a CheckRotation script and a BoxCollider
            //checkRotation.enabled = true;
            rotationCheckCollider = objectToLevitate.GetComponentInChildren<MeshCollider>();
            //checkRotation.RotationCollision += IsRotationCollision; //Subscribe to CheckRotation Events
            //checkRotation.RotationCollisionExit += NoRotationCollision;
            levitatingObj = objectToLevitate;
            OnSendLevitatingObj();
        }
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        OnTeleMovingObject();
        if (isRotating)
        {
            RotateObject();
        }
        if (isCarrying)
        {
            CarryObject(objectToLevitate, objectTransfrom);
        }
        else
        {
            MoveLevitateTransform();
            MoveLevitateObject(objectToLevitate, objectTransfrom);
        }

        teleEnergy -= (energyDrainRate * Time.deltaTime);
        Debug.Log("LevitatingObj");
    }

    private void CarryObject(GameObject objToLevitate, Vector3 objTransform)
    {
        ResetLevTransform();
        OnTeleStoppedMovingObject();
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.transform.position = objTransform;
    }

    private void GetObjectRigidBody(GameObject objToLevitate)
    {
        try
        {
            objectRigidBody = objToLevitate.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {

            throw new UnityException("No rigidbody!");
        }
    }

    private void MoveLevitateObject(GameObject objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.transform.position = objTransform;
    }

    private void MoveLevitateTransform()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Mouse Y");
        zInput = Input.GetAxis("levZ");

        //Checks minimum idstance but needs work
        //if (Physics.Raycast(MinDistRayTransform.position, Vector3.forward, minDist, levitatingObjLayer))
        //{
        //    levDirection = new Vector3(xInput, yInput, levitatableObj.transform.position.z);
        //}
        //else
        //{
        //    levDirection = new Vector3(xInput, yInput, zInput);
        //}
        levDirection = new Vector3(xInput, yInput, zInput);
        levitateTransform.Translate(levDirection * transfromMoveSpeed * Time.deltaTime);
    }

    private void RotateObject()
    {
        objectRigidBody.velocity = Vector3.zero;    //Stops the object from moving once you let it go
        objectRigidBody.angularVelocity = Vector3.zero;

        if (Input.GetButtonDown("Vertical"))
        {


            if (Input.GetAxis("Vertical") > 0)
            {
                //rotationCheckCollider.transform.RotateAround(rotationCheckCollider.transform.position, new Vector3(1, 0, 0), rotationAngleSnap); //Rotates RotationCheckCollider which will determine if there is a collision or not
                //if (!checkRotation.WillCollide())
                //{
                //    levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), rotationAngleSnap);
                //}
                //else
                //{
                //    rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), -rotationAngleSnap); //Rotates RotationCheckCollider back if there was a collision
                //}
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), rotationAngleSnap);

            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                //rotationCheckCollider.transform.RotateAround(rotationCheckCollider.transform.position, new Vector3(1, 0, 0), -rotationAngleSnap);
                //if (!checkRotation.WillCollide())
                //{
                //    levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), -rotationAngleSnap);
                //}
                //else
                //{
                //    rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), rotationAngleSnap);
                //}
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), -rotationAngleSnap);
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                //rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), -rotationAngleSnap);

                //if (!checkRotation.WillCollide())
                //{
                //    levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), -rotationAngleSnap);
                //}
                //else
                //{
                //    rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), rotationAngleSnap);
                //}
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), -rotationAngleSnap);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), rotationAngleSnap);

                //if (!checkRotation.WillCollide())
                //{
                //    levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), rotationAngleSnap);
                //}
                //else
                //{
                //    rotationCheckCollider.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), -rotationAngleSnap);
                //}
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), rotationAngleSnap);
            }
        }
        if (Input.GetButtonDown("ResetRotate"))
        {
            levitatableObj.transform.rotation = Quaternion.identity;
        }


    }

    private void DropObject(GameObject objectToDrop)
    {
        try
        {
            objectRigidBody = objectToDrop.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            throw new UnityException("No rigidbody!");
        }
        objectToDrop.layer = 0;
        isLevitatingObject = false;
        isRotating = false;
        objectRigidBody.useGravity = true;
        ResetLevTransform();
        levitatingObj = null;
        wasLevitating = false;
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
                    isPulling = true;
                    levitatingObj = objToLevitate;
                }

            }
        }
        else
        {
            Debug.Log("No levitatable object");
        }

    }

    public void SetLevitatableObject(GameObject gameObject)
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

    private void IsRotationCollision()
    {
        canRotate = false;
    }

    private void NoRotationCollision()
    {
        canRotate = true;
    }

    private void ResetLevTransform()
    {
        levitateTransform.localPosition = startingTransform;
    }

    float EnergyPercent()
    {
        return teleEnergy / maxEnergy;
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

    private void OnSendLevitatingObj()
    {
        if (SendLevitatingObj != null)
        {
            SendLevitatingObj.Invoke(levitatingObj);
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
        //checkRotation.RotationCollision -= IsRotationCollision;
        //checkRotation.RotationCollisionExit -= NoRotationCollision;
        //DetectObject.LevObjectExit += ResetLevitatableObj;
    }
}