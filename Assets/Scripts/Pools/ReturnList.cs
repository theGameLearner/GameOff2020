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
using TGL.Singletons;

public class ReturnList : GenericSingletonMonobehaviour<ReturnList>
{
	List<ReturnObject> returnList;
	private void Start()
	{
		if(returnList == null)
		{
			returnList = new List<ReturnObject>();
		}
	}

	void Update()
	{
		if(returnList.Count == 0)
		{
			return;
		}

		for (int i = 0; i < returnList.Count; i++)
		{
			returnList[i].timeLeft -= Time.deltaTime;
			if(returnList[i].timeLeft < 0)
			{
				ObjectPool.instance.ReturnPooledObject(returnList[i].poolIndex, returnList[i].objToReturn);
				returnList.RemoveAt(i);
			}
		}
	}

	public void AddToReturnList(GameObject objToAdd, int pIndex, float duration)
	{
		ReturnObject rObj = new ReturnObject(objToAdd, pIndex, duration);
		if (returnList == null)
		{
			returnList = new List<ReturnObject>();
		}
		returnList.Add(rObj);
	}

	public void ReturnIfExisting(GameObject go_ToCheck)
	{
		for(int i=0; i<returnList.Count; i++)
		{
			if(returnList[i].objToReturn == go_ToCheck)
			{
				ObjectPool.instance.ReturnPooledObject(returnList[i].poolIndex, returnList[i].objToReturn);
				returnList.RemoveAt(i);
			}
		}
	}
}
