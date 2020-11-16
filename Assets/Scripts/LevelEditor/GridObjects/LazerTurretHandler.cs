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
public class LazerTurretHandler : MonoBehaviour
{
    public float ratationSpeed = 5;
	public Transform turretTrans;

	private Collider myCollider;

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
	}

	public void Revive()
	{
		turretTrans.gameObject.SetActive(true);
		myCollider.enabled = true;
	}
}