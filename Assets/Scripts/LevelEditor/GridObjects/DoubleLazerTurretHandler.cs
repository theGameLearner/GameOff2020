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
        RaycastHit startHitPoint; 
        if(Physics.Raycast(lazerOrigin, turretTrans.forward, out startHitPoint, lazerSize))
        {
            startLazerPoint = startHitPoint.point;
			if(startHitPoint.collider.transform == GameSettings.instance.playerTransform){
				GameManager.instance.GameOver();
			}
        }

        RaycastHit endHitPoint;
        if (Physics.Raycast(lazerOrigin, (-1 * turretTrans.forward), out endHitPoint, lazerSize))
        {
            endLazerPoint = endHitPoint.point;
			if(endHitPoint.collider.transform == GameSettings.instance.playerTransform){
				GameManager.instance.GameOver();
			}
        }
    }
}
