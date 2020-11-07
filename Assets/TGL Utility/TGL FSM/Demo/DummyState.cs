/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

#define TGL_DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.FSM;

public class DummyState : AbsState
{
	/// <summary>
	/// the machine class is subchild of 'AbsStateMachine' so using machine will fetch us all subtype machine that we are currently controlling
	/// </summary>
	public DummyMachine _machine { get { return (DummyMachine) machine; } }

	public override void Enter()
	{
		#if TGL_DEBUG
			Debug.Log("01. state - First Line executed");
		#endif
		//base.Enter(); //Not necessary here but if you have a long hierarchy, better to include all parent functions
		#if TGL_DEBUG
			Debug.Log("02. state - Entered the state "+ this.GetType().Name);
		#endif
		_machine.stateName = this.GetType().Name;
	}

	public override void StateUpdate()
	{
		//base.StateUpdate();
		if(Input.GetKeyDown(KeyCode.C))
		{
			#if TGL_DEBUG
				Debug.Log("03. state - requesting to change the state in " + this.GetType().Name);
			#endif
			_machine.ChangeStateToNew();
		}
	}

	public override void Exit()
	{
		#if TGL_DEBUG
			Debug.Log("4. state - Exit's First Line executed");
		#endif
		//base.Exit(); //Not necessary here but if you have a long hierarchy, better to include all parent functions
		#if TGL_DEBUG
			Debug.Log("5. state - Exiting the state " + this.GetType().Name);
		#endif
	}
}
