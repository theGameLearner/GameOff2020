/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserDetails
{
    public int uId;
    public string userName;
    public string uPass;

    public UserDetails (string uName, string password)
    {
        this.userName = uName;
        this.uPass = password;
    }
}

[Serializable]
public class LastUId
{
    public int LastUserID;

    public LastUId(int lastVal)
	{
        LastUserID = lastVal;
	}
}
