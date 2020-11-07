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

public class DummyState02 : AbsState
{
	public DummyMachine _machine { get { return (DummyMachine)machine; } }
	public override void Enter()
	{
		#if TGL_DEBUG
			Debug.Log("06. state - First Line executed");
		#endif
		//base.Enter(); //Not necessary here but if you have a long hierarchy, better to include all parent functions
		#if TGL_DEBUG
			Debug.Log("07. state - Entered the state "+ this.GetType().Name);
		#endif

		_machine.stateName = this.GetType().Name;
	}
	/*
	 * //! It is not necessary to implement all state methods just like Unity's MonoBehaviour
	 */

	public override void StateUpdate()
	{
		//base.StateUpdate();
		if (Input.GetKeyDown(KeyCode.C))
		{
			#if TGL_DEBUG
				Debug.Log("08. state - requesting to change the state in " + this.GetType().Name);
			#endif
			_machine.ChangeStateToOld();
		}
	}
}
