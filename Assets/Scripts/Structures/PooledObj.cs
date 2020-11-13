/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObj
{
	public GameObject ObjectToPool;
	public List<GameObject> pooledObject;
	public int maxCount = 10;
	public bool shouldExpand = true;
	public Transform poolParent;
}
