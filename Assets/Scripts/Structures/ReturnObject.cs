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

public class ReturnObject
{
	public GameObject objToReturn;
	public int poolIndex;
	public float timeLeft;
	public ReturnObject(GameObject objToAdd, int pIndex, float duration)
	{
		objToReturn = objToAdd;
		poolIndex = pIndex;
		timeLeft = duration;
	}
}
