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

[RequireComponent(typeof(Collider))]
public class LazerTurretHandler : MonoBehaviour,IGridObject,IDestroyableEnemy
{
    public float ratationSpeed = 5;
	public Transform turretTrans;

	private Collider myCollider;

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
	}

	void Update()
    {
		if (turretTrans.gameObject.activeSelf)
		{
			turretTrans.Rotate(Vector3.up, ratationSpeed * Time.deltaTime);
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("collission is made with "+ collision.transform.name);
		if(collision.transform == GameSettings.instance.playerTransform)
		{
			KillTurret();
		}
	}

	private void KillTurret()
	{
		turretTrans.gameObject.SetActive(false);
		myCollider.enabled = false;
		GameManager.instance.EnemyDestroyed();
	}

	public void Revive()
	{
		turretTrans.gameObject.SetActive(true);
		myCollider.enabled = true;
	}
}