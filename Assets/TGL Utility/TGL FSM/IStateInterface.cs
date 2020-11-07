/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #25-Oct-2020#
 */


namespace TGL.FSM
{
	public interface IStateInterface
	{
		/// <summary>
		/// this method is used to define the actions user wants to perform when the state starts for first time
		///   Please avoid using coroutines and any other function calls that may not end in a single frame here
		/// </summary>
		void Initialize();

		/// <summary>
		/// this method is used to enter the machine to this class state 
		/// </summary>
		void Enter();

		/// <summary>
		/// Used to run any last moment edits before machine changes from this state to another
		///  Please avoid using coroutines and any other function calls that may not end in a single frame here
		/// </summary>
		void Exit();

		#region Updates

		/// <summary>
		/// equivalent to Update used n Unity but runs for this particular state when active
		/// </summary>
		void StateUpdate();

		/// <summary>
		/// equivalent to FixedUpdate used n Unity but runs for this particular state when active
		/// </summary>
		void StateFixedUpdate();

		/// <summary>
		/// equivalent to LateUpdate used n Unity but runs for this particular state when active
		/// </summary>
		void StateLateUpdate();

		#endregion //Updates
		
		#region collission Functions

			void OnCollisionEnter(UnityEngine.Collision collision);
			void OnCollisionStay(UnityEngine.Collision collision);
			void OnCollisionExit(UnityEngine.Collision collision);

			void OnCollisionEnter2D(UnityEngine.Collision2D collision);
			void OnCollisionStay2D(UnityEngine.Collision2D collision);
			void OnCollisionExit2D(UnityEngine.Collision2D collision);

		#endregion //collission Functions

		#region trigger Functions

			void OnTriggerEnter(UnityEngine.Collider collider);
			void OnTriggerStay(UnityEngine.Collider collider);
			void OnTriggerExit(UnityEngine.Collider collider);

			void OnTriggerEnter2D(UnityEngine.Collider2D collider);
			void OnTriggerStay2D(UnityEngine.Collider2D collider);
			void OnTriggerExit2D(UnityEngine.Collider2D collider);

		#endregion //trigger Functions

		/// <summary>
		/// the machine the state controls
		/// </summary>
		IStateMachineInterface machine { get; }
		/// <summary>
		/// to get the machine this state class is being used to control
		/// </summary>
		/// <typeparam name="T"> T is whichever class is being used to control the machine we want to get</typeparam>
		/// <returns></returns>
		T GetMachine<T>() where T : IStateMachineInterface;
	}
}
