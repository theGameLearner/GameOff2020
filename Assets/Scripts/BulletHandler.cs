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

public class BulletHandler : MonoBehaviour
{
	TrailRenderer tRenderer;
	const float faddeOutTime = 0.1f;
	private void Start()
	{
		tRenderer = transform.GetComponent<TrailRenderer>();
		if (tRenderer != null)
		{
			tRenderer.Clear();
			tRenderer.time = faddeOutTime;
		}
	}

	private void OnEnable()
	{
		if (tRenderer != null)
		{
			tRenderer.Clear();
		}
	}

	private void OnDisable()
	{
		if (tRenderer != null)
		{
			tRenderer.Clear();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("bullet collided with " + collision.transform.name);

		if(collision.transform == GameSettings.instance.playerTransform)
		{
			Debug.Log("Hit with player, need to call Game Over");
		}	

		ReturnList.instance.ReturnIfExisting(gameObject);
	}
}