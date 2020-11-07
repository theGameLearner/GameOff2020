﻿/*
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
using System;

public class UIRendering : GenericSingletonMonobehaviour<UIRendering>
{

	public RectTransform directionStartRectT;
	public RectTransform directionTargetRectT;

	public Vector3 directionStartPoint;
	public Vector3 directionTargetPoint;

	/// <summary>
	/// Draws a circle with center as the position where the mouse input started
	/// </summary>
	/// <param name="mouseStartPos">the position of mouse at starting</param>
	public void DrawUiMouseDown(Vector3 mouseStartPos)
	{
		//throw new NotImplementedException();
	}

	/// <summary>
	/// draws a circle from starting position to the current mouse position direction
	/// </summary>
	/// <param name="mousePos">current mouse position</param>
	public void DrawUiMouseMove(Vector3 mousePos)
	{
		//throw new NotImplementedException();
	}

	/// <summary>
	/// stops ui rendering of mouse direction
	/// </summary>
	public void StopUIMouseRendering()
	{
		//throw new NotImplementedException();
	}
}
