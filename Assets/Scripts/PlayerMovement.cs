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
    public float bounceSpeed;
    public Transform camTrans;

    Vector3 reflectionDirection;
    Vector3 travelDirection;
    Rigidbody myRb;

    Vector3 camForward;
    Vector3 camRight;

    Transform playerRendererTrans;
    Transform rotationTransform;
    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        if(transform.GetChild(0) != null)
		{
            playerRendererTrans = transform.GetChild(0);
            rotationTransform = new GameObject().transform;
            rotationTransform.parent = transform;
        }

    }

    void Update()
    {
        if(playerRendererTrans != null && travelDirection.magnitude > 0.5f)
		{
            rotationTransform.forward = travelDirection;

            playerRendererTrans.Rotate(rotationTransform.right, speed*Time.timeScale * (myRb.velocity.magnitude / speed), Space.World);
			//playerRendererTrans.right = travelDirection.normalized;

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

    public void BouncePlayerSpeed()
    {
        if (travelDirection.magnitude > 0 && bounceSpeed > 1)
        {
            myRb.velocity = myRb.velocity.normalized * bounceSpeed;
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
