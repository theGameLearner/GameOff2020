/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #25-Oct-2020#
 */

namespace TGL.FSM
{
	public interface IStateMachineInterface
	{
		/// <summary>
		/// used to define which is the initial state of this machine at start of the machine
		/// </summary>
		/// <typeparam name="T">the initial state class</typeparam>
		void SetInitialState<T>() where T : AbsState;

		/// <summary>
		/// used to define which is the initial state of this machine at start of the machine
		/// </summary>
		/// <param name="T">the initial state class</param>
		void SetInitialState(System.Type T);

		/// <summary>
		/// Change state of machine from current state to the new state
		/// </summary>
		/// <typeparam name="T">new state class that we want to change to</typeparam>
		void ChangeState<T>() where T : AbsState;
		/// <summary>
		/// Change state of machine from current state to the new state
		/// </summary>
		/// <param name="T">new state class that we want to change to</param>
		void ChangeState(System.Type T);

		/// <summary>
		/// returns a bool if the machine is in same state as the class passed
		/// </summary>
		/// <typeparam name="T">the state we want to test for</typeparam>
		/// <returns></returns>
		bool IsCurrentState<T>() where T : AbsState;
		/// <summary>
		/// returns a bool if the machine is in same state as the class passed
		/// </summary>
		/// <param name="T">the state we want to test for</param>
		/// <returns></returns>
		bool IsCurrentState(System.Type T);

		/// <summary>
		/// returns the current state of the machine
		/// </summary>
		/// <typeparam name="T">the class which the machine is currently using</typeparam>
		/// <returns></returns>
		T CurrentState<T>() where T : AbsState;

		/// <summary>
		/// returns the previous state of the machine
		/// </summary>
		/// <typeparam name="T">the class which the machine was previously using</typeparam>
		/// <returns></returns>
		T PreviousState<T>() where T : AbsState;


		/// <summary>
		/// returns from the dictionary of all states added to the machine, the state that corresponds to the class passed
		/// </summary>
		/// <typeparam name="T">the class for which we want the state</typeparam>
		/// <returns></returns>
		T GetState<T>() where T : AbsState;

		/// <summary>
		/// Add the state to the current machine as a switchable class
		/// </summary>
		/// <typeparam name="T">the new state to add to current machine</typeparam>
		void AddState<T>() where T : AbsState, new();
		/// <summary>
		/// Add the state to the current machine as a switchable class
		/// </summary>
		/// <param name="T">the new state to add to current machine</param>
		void AddState(System.Type T);

		/// <summary>
		/// remove this state as a changable state from the machine
		/// </summary>
		/// <typeparam name="T">the class to remove</typeparam>
		void RemoveState<T>() where T : AbsState;
		/// <summary>
		/// remove this state as a changable state from the machine
		/// </summary>
		/// <param name="T">the class to remove</param>
		void RemoveState(System.Type T);

		/// <summary>
		/// returns true if the state has been added to machine as a switchable class
		/// </summary>
		/// <typeparam name="T">the class we want to check is added to the machine</typeparam>
		/// <returns></returns>
		bool ContainsState<T>() where T : AbsState;
		/// <summary>
		/// returns true if the state has been added to machine as a switchable class
		/// </summary>
		/// <param name="T">the class we want to check is added to the machine</param>
		/// <returns></returns>
		bool ContainsState(System.Type T);

		/// <summary>
		/// removes all added states from the machine
		/// </summary>
		void RemoveAllStates();

		/// <summary>
		/// name of the machine
		/// </summary>
		string name { get; set; }
	}
}
