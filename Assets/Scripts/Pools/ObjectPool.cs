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
public class ObjectPool : GenericSingletonMonobehaviour<ObjectPool>
{
	public List<PooledObj> poolList;

	private void Start()
	{
		for(int i=0; i<poolList.Count; i++)
		{
			PooledObj objToPool = poolList[i];
			for(int j=0; j< objToPool.maxCount; j++)
			{
				GameObject gObj = (GameObject)Instantiate(objToPool.ObjectToPool);
				if(objToPool.pooledObject == null)
				{
					objToPool.pooledObject = new List<GameObject>();
				}
				gObj.SetActive(false);
				gObj.transform.parent = objToPool.poolParent;
				objToPool.pooledObject.Add(gObj);
			}
		}
	}

	public GameObject GetPooledObject(int index)
	{
		PooledObj poolStruct = poolList[index];
		for(int i=0; i< poolStruct.pooledObject.Count; i++)
		{
			if(!poolStruct.pooledObject[i].activeSelf)
			{
				return poolStruct.pooledObject[i];
			}
		}

		//we reach this part, if no object was found
		int expandedCount = poolStruct.maxCount;
		for(int j=0;j< expandedCount;j++)
		{
			GameObject gObj = (GameObject)Instantiate(poolStruct.ObjectToPool);
			gObj.SetActive(false);
			poolStruct.pooledObject.Add(gObj);
		}
		poolStruct.maxCount += expandedCount;
		return poolStruct.pooledObject[expandedCount];
	}

	public void ReturnPooledObject(int poolIndex, GameObject pooledObject)
	{
		if(poolList[poolIndex].pooledObject.Contains(pooledObject) && pooledObject.activeSelf)
		{
			pooledObject.SetActive(false);
		}
		else
		{
			Debug.LogError("could not find the GameObject in the list:\nGameObject : "+pooledObject+"\n poolIndex: "+poolIndex);
		}
	}
}
