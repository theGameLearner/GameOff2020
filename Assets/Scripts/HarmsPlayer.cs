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

public class HarmsPlayer : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform == GameSettings.instance.playerTransform)
		{
			GameManager.instance.GameOver();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform == GameSettings.instance.playerTransform)
		{
			GameManager.instance.GameOver();
		}
	}
}
