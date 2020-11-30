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
public class DoubleLazerTurretHandler : MonoBehaviour,IGridObject
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

	#region IgridObject
		
		int index;
		int x;
		int y;

		public int GetIndex()
		{
			return index;
		}

		public string GetJsonData()
		{
			return "";
		}

		public void GetXY(out int x, out int y)
		{
			x = this.x;
			y = this.y;
		}

		public void Initialize(string jsonData)
		{
			
		}

		public void SetIndex(int index)
		{
			this.index = index;
		}

		public void SetXY(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

	#endregion

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
			if (startHitPoints[sI].collider.transform == GameSettings.instance.playerTransform  && sI == 0)
			{
				GameManager.instance.GameOver();
			}
			startLazerPoint = startHitPoints[sI].point;
		}

		for (int eI = 0; eI < endHitPoints.Length; eI++)
		{
			if (endHitPoints[eI].collider.transform == GameSettings.instance.playerTransform  && eI == 0)
			{
				GameManager.instance.GameOver();
			}
			endLazerPoint = endHitPoints[eI].point;
		}
	}
}
