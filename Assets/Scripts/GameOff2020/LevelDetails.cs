/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System;
using UnityEngine;

[Serializable]
public class LevelDetails
{
	public string levelName;
	public string levelJson;
	

	public LevelDetails(string lName, string lJson)
	{
		levelName = lName;
		levelJson = lJson;
	}
}