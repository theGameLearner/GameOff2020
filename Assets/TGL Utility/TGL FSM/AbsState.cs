/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #25-Oct-2020#
 */

using UnityEngine;

namespace TGL.FSM
{
	public abstract class AbsState : IStateInterface
	{
		public IStateMachineInterface machine { get; internal set; }

		public virtual void Enter(){ }
		public virtual void Exit(){ }
		public T GetMachine<T>() where T : IStateMachineInterface
		{
			try
			{
				return (T)machine;
			}
			catch(System.InvalidCastException e)
			{
				throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\n" + e.Message);
			}
		}
		public virtual void Initialize(){ }
		public virtual void OnCollisionEnter(Collision collision){ }
		public virtual void OnCollisionEnter2D(Collision2D collision){ }
		public virtual void OnCollisionExit(Collision collision){ }
		public virtual void OnCollisionExit2D(Collision2D collision){ }
		public virtual void OnCollisionStay(Collision collision){ }
		public virtual void OnCollisionStay2D(Collision2D collision){ }
		public virtual void OnTriggerEnter(Collider collider){ }
		public virtual void OnTriggerEnter2D(Collider2D collider){ }
		public virtual void OnTriggerExit(Collider collider){ }
		public virtual void OnTriggerExit2D(Collider2D collider){ }
		public virtual void OnTriggerStay(Collider collider){ }
		public virtual void OnTriggerStay2D(Collider2D collider){ }
		public virtual void StateFixedUpdate(){ }
		public virtual void StateLateUpdate(){ }
		public virtual void StateUpdate(){ }
	}
}
