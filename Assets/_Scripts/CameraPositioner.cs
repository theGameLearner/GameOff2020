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

public class CameraPositioner : MonoBehaviour
{
    public Transform targetTransform;
    Vector3 offset;
	private void Start()
	{
		offset = transform.position - targetTransform.position;
	}

	private void Update()
	{
		transform.position = targetTransform.position + offset;
	}
}
