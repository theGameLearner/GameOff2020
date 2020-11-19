/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Collider))]
public class DoubleLazerTurretHandler : MonoBehaviour
{
	public float ratationSpeed = 5;
	public int lazerSize = 5;
	public Transform turretTrans;
	public float lazerHeight = 0.5f;

	private Collider myCollider;
	private LineRenderer lazerLine;
	private Vector3 startLazerPoint;
	private Vector3 endLazerPoint;
	private Vector3 lazerOrigin;

	private void Start()
	{
		myCollider = GetComponent<Collider>();
		lazerLine = GetComponent<LineRenderer>();
		startLazerPoint = new Vector3();
		endLazerPoint = new Vector3();
		lazerOrigin = turretTrans.position;
		lazerOrigin.y = lazerHeight;
	}

	void Update()
	{
		if (turretTrans.gameObject.activeSelf)
		{
			turretTrans.Rotate(Vector3.up, ratationSpeed * Time.deltaTime);
			
			startLazerPoint = turretTrans.position + (turretTrans.forward * lazerSize);
			startLazerPoint.y = lazerHeight;
			endLazerPoint = turretTrans.position + (turretTrans.forward * lazerSize * -1);
			endLazerPoint.y = lazerHeight;

			CheckLazerLength();

			lazerLine.SetPosition(0, startLazerPoint);
			lazerLine.SetPosition(1, endLazerPoint);
		}
	}

	void CheckLazerLength()
	{
		RaycastHit[] startHitPoints = Physics.RaycastAll(lazerOrigin, turretTrans.forward, lazerSize);
		RaycastHit[] endHitPoints = Physics.RaycastAll(lazerOrigin, (-1*turretTrans.forward), lazerSize);

		for(int sI=0; sI<startHitPoints.Length;sI++)
		{
			//if(startHitPoints[sI].collider.transform != GameSettings.instance.playerTransform)
			{
				startLazerPoint = startHitPoints[sI].point;
			}	
		}

		for (int eI = 0; eI < endHitPoints.Length; eI++)
		{
			//if (endHitPoints[eI].collider.transform != GameSettings.instance.playerTransform)
			{
				endLazerPoint = endHitPoints[eI].point;
			}
		}
	}
}
