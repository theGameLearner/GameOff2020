/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Transform camTrans;

    Vector3 reflectionDirection;
    Vector3 travelDirection;
    Rigidbody myRb;

    Vector3 camForward;
    Vector3 camRight;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //KeyboardInput();

    }

	private void KeyboardInput()
	{
        camForward = camTrans.forward;
        camForward.y = 0;
        camForward.Normalize();

        camRight = camTrans.right;
        camRight.y = 0;
        camRight.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            myRb.velocity = camForward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myRb.velocity = -1 * camForward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myRb.velocity = -1 * camRight * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myRb.velocity = camRight * speed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
            //myRb.velocity = Vector3.zero;
        }
    }

    public void ChangePlayerDirection(Vector3 newPlayerDirection)
	{
        travelDirection = newPlayerDirection;
        if (travelDirection.magnitude > 0)
        {
            myRb.velocity = travelDirection * speed;
        }
    }

    /*
	private void OnCollisionEnter(Collision collision)
	{
        Ray ray = new Ray(transform.position, travelDirection);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 2))
        {
            reflectionDirection = Vector3.Reflect(travelDirection, hit.normal);
        }
        travelDirection = reflectionDirection;
        myRb.velocity = travelDirection * speed;
    }
    */
}
