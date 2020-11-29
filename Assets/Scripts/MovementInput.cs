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
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class MovementInput : MonoBehaviour
{
    public float maxlineLength = 50;
    public int maxReflections = 5;
    LineRenderer directionRenderer;
    UIRendering uIRendering;
    Transform playerTransform;
    PlayerMovement playerMovementScript;
    public bool playerAcceptsInput = true; 

    bool _mouseDown;
    Vector3 mouseStartPos;
    Vector3 mouseEndPos;
    Vector3 mousePos;
    Vector3 lastFrameVelocityDir;

    void Start()
    {
        playerTransform = transform;
        directionRenderer = GetComponent<LineRenderer>();
        uIRendering = UIRendering.instance;
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(!playerAcceptsInput)
		{
            Time.timeScale = GameSettings.instance.timeScale / 2f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            return;
		}
        lastFrameVelocityDir = GetComponent<Rigidbody>().velocity.normalized;
        _mouseDown = Input.GetMouseButton(0);
        mousePos = Input.mousePosition;
        
        if (_mouseDown)
		{
            if(Time.timeScale > GameSettings.instance.timeScale)
			{
                Time.timeScale = GameSettings.instance.timeScale;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
            DrawLine(mouseStartPos, mousePos);
            uIRendering.DrawUiMouseMove(mousePos);
        }
        else
		{
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
		}

        if(Input.GetMouseButtonDown(0))
		{
            mouseStartPos = mousePos;
            uIRendering.DrawUiMouseDown(mousePos);
            directionRenderer.enabled = true;
        }
        if(Input.GetMouseButtonUp(0))
		{
            mouseEndPos = mousePos;
            ChangeDirection(mouseStartPos, mouseEndPos);
            uIRendering.StopUIMouseRendering();
            directionRenderer.enabled = false;
            directionRenderer.positionCount = 2;
        }
    }

    /// <summary>
    /// Sets the UI using mouse start pos and current mouse pos to determine the new direction
    /// </summary>
    /// <param name="mouseStartPos"></param>
    /// <param name="mousePos"></param>
    void DrawLine(Vector3 mouseStartPos, Vector3 mousePos)
	{
        directionRenderer.SetPosition(0, playerTransform.position);
        Vector3 travelDirection = GetPlayerDesiredDirection(mouseStartPos, mousePos);
        DrawRenderingLine(playerTransform.position, travelDirection, maxReflections, 1);
    }

    /// <summary>
    /// Changes the direction of player using the mouse start and end position
    /// </summary>
    /// <param name="mouseStartPos"></param>
    /// <param name="mouseEndPos"></param>
    void ChangeDirection(Vector3 mouseStartPos, Vector3 mouseEndPos)
	{
        Vector3 newDirection = GetPlayerDesiredDirection(mouseStartPos, mouseEndPos);
        playerMovementScript.ChangePlayerDirection(newDirection);
    }

    void DrawRenderingLine(Vector3 startPos, Vector3 travelDirection, int remainingReflections, int indexVal)
	{
        if(remainingReflections<=0)
		{
            return;
        }

        Vector3 nextPosition = startPos;
        Ray ray = new Ray(nextPosition, travelDirection);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxlineLength))
		{
            travelDirection = Vector3.Reflect(travelDirection, hit.normal);
            nextPosition = hit.point;
		}
        else
		{
            nextPosition += (travelDirection.normalized * maxlineLength);

		}
        if(directionRenderer.positionCount == indexVal)
		{
            directionRenderer.positionCount += 1;
        }
        directionRenderer.SetPosition(indexVal, nextPosition);
		//recursive call
		DrawRenderingLine(nextPosition, travelDirection, (--remainingReflections), (++indexVal));
	}

    Vector3 GetPlayerDesiredDirection(Vector3 mouseStartPos, Vector3 mouseEndPos)
	{
        Vector3 travelDirection = (mouseEndPos - mouseStartPos);
        travelDirection.z = travelDirection.y;
        travelDirection.y = 0;
        travelDirection = travelDirection.normalized;
        return travelDirection;
    }

	private void OnCollisionExit(Collision collision)
	{
		if(collision.transform.GetComponent<WallObject>() != null)
		{
            playerMovementScript.BouncePlayerSpeed();
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if(collision.transform.parent.GetComponent<floor>() != null)
		{
            //we collide with floor.
            return;
		}
        //Debug.Log("collided with " + collision.transform.name, collision.transform);
        Vector3 collisionNormal = collision.contacts[0].normal;
        var direction = Vector3.Reflect(lastFrameVelocityDir.normalized, collisionNormal);
        playerMovementScript.ChangePlayerDirection(direction.normalized);

        GameObject sparksGo = ObjectPool.instance.GetPooledObject(GameSettings.instance.sparksVfxPoolIndex);
		sparksGo.SetActive(true);
        sparksGo.transform.SetParent(null);
		sparksGo.transform.position = transform.position;
    }
}
