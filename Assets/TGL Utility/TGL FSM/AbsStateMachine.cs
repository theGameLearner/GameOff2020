/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #25-Oct-2020#
 */

#define SYSTEM_DEBUG // can be used to see what states are chaning at run time

using UnityEngine;

namespace TGL.FSM
{
	public abstract class AbsStateMachine : MonoBehaviour, IStateMachineInterface
	{
		#region variable definition

		/// <summary>
		/// dictionary to hold all states that are passed to the current machine
		/// </summary>
		protected System.Collections.Generic.Dictionary<System.Type, AbsState> allStates = new System.Collections.Generic.Dictionary<System.Type, AbsState>();
		
		/// <summary>
		/// stores the current state of the machine as a variable with parent abstract class
		/// </summary>
		protected AbsState currentState { get; set; }

		/// <summary>
		/// stores the next state we want to change to. 
		///  Value is used when we are trying to change the state
		/// </summary>
		public AbsState nextState { get; set; }
		/// <summary>
		/// stores the current state we want to change from. 
		///  Value is used when we are trying to change the state
		/// </summary>
		public AbsState prevState { get; set; }
		/// <summary>
		/// used to store the first state we want our machine to initialize using
		/// </summary>
		protected AbsState initialState { get; set; }

		/// <summary>
		/// it is true when we enter a desired state
		/// </summary>
		protected bool onEnter { get; set; }
		/// <summary>
		/// it is true when we exit from a desired state
		/// </summary>
		protected bool onExit { get; set; }

		#endregion //variable definition

		#region machine initialization

		/// <summary>
		/// called at start of Machine defined in TGL_FSM
		///  overriding the base start of UnityEngine.MonoBehaviour so that on scene start, this function gets called in place of default function.
		/// </summary>
		public virtual void Start()
		{
			Initialize();
		}

		/// <summary>
		/// initializes the machine due to start being called. will add states and change to initial state
		/// </summary>
		public virtual void Initialize()
		{
			//call add states so we can perform the add states method call for this machine.
			AddStates();
			//set current state to the initial state of this machine set in AddStates() methiod
			currentState = initialState;

			//check if current state is not set
			if (null == currentState)
			{
				throw new System.Exception("\n" + name + ".nextState is null on Initialize()!\tDid you forget to call SetInitialState() to a valid state?\n");
			}

			//add all states to allStates dictionary
			foreach (System.Collections.Generic.KeyValuePair<System.Type, AbsState> pair in allStates)
			{
				pair.Value.Initialize();
			}

			//as we are trying to enter our initial state, set onEnter to true
			onEnter = true;
			onExit = false;
		}

		/// <summary>
		/// define this method to make a call to add all desired states to the machine 
		///  as well as the initial state - will be auto-called by FSM system
		/// </summary>
		public abstract void AddStates();

		#endregion //machine initialization

		#region Machine Updates

		/// <summary>
		/// checks if we need to change the state, or if StateUpdate has been defined on current state and run it.
		/// </summary>
		public virtual void Update()
		{
			//if we are trying to change the state to a new state, we will set onExit to true to call Exit() method of AbsState child
			if (onExit)
			{
				currentState.Exit();
				prevState = currentState;
				currentState = nextState;
				nextState = null;

				onEnter = true;
				onExit = false;
			}

			//if we are trying to change the state to a new state, we will set onEnter to true to call Enter() method of AbsState child
			if (onEnter)
			{
				currentState.Enter();

				onEnter = false;
			}

			//for the current state, run the StateUpdate as defined by AbsState child
			try
			{
				currentState.StateUpdate();
			}
			catch (System.NullReferenceException e)
			{
				if (null == currentState)
				{
					throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
				}
				else
				{
					throw new System.Exception("\n" + name + ".currentState on running StateUpdate encountered an error!\n" + e.Message);
				}
			}
		}

		/// <summary>
		/// if we are neither entering nor exiting the current state, run the StateFixedUpdate() method of AbsState child
		/// </summary>
		public virtual void FixedUpdate()
		{
			if (!(onEnter && onExit))
			{
				try
				{
					currentState.StateFixedUpdate();
				}
				catch (System.NullReferenceException e)
				{
					if (null == currentState)
					{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
					}
					else
					{
						throw new System.Exception("\n" + name + ".currentState on running StateFixedUpdate encountered an error!\n" + e.Message);
					}
				}
			}
		}

		/// <summary>
		/// if we are neither entering nor exiting the current state, run the StateLateUpdate() method of AbsState child
		/// </summary>
		public virtual void LateUpdate()
		{
			if (!(onEnter && onExit))
			{
				try
				{
					currentState.StateLateUpdate();
				}
				catch (System.NullReferenceException e)
				{
					if (null == currentState)
					{
						throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
					}
					else
					{
						throw new System.Exception("\n" + name + ".currentState on running LateUpdate encountered an error!\n" + e.Message);
					}
				}
			}
		}

		#endregion //Machine Updates

		#region collission and triggers
		/// <summary>
		/// Machine's OnCollisionEnter will redirect call to current state's OnCollisionEnter()
		/// </summary>
		/// <param name="collision"> the Collision event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionEnter(Collision collision) { currentState.OnCollisionEnter(collision); }

		/// <summary>
		/// Machine's OnCollisionStay will redirect call to current state's OnCollisionStay()
		/// </summary>
		/// <param name="collision"> the Collision event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionStay(Collision collision) { currentState.OnCollisionStay(collision); }
		/// <summary>
		/// Machine's OnCollisionExit will redirect call to current state's OnCollisionExit()
		/// </summary>
		/// <param name="collision"> the Collision event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionExit(Collision collision) { currentState.OnCollisionExit(collision); }

		/// <summary>
		/// Machine's OnCollisionEnter2D will redirect call to current state's OnCollisionEnter2D()
		/// </summary>
		/// <param name="collision_2d">the Collision2D event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionEnter2D(Collision2D collision_2d) { currentState.OnCollisionEnter2D(collision_2d); }
		/// <summary>
		/// Machine's OnCollisionStay2D will redirect call to current state's OnCollisionStay2D()
		/// </summary>
		/// <param name="collision_2d">the Collision2D event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionStay2D(Collision2D collision_2d) { currentState.OnCollisionStay2D(collision_2d); }
		/// <summary>
		/// Machine's OnCollisionExit2D will redirect call to current state's OnCollisionExit2D()
		/// </summary>
		/// <param name="collision_2d">the Collision2D event passed by Unity's Monobehaviour</param>
		public virtual void OnCollisionExit2D(Collision2D collision_2d) { currentState.OnCollisionExit2D(collision_2d); }

		/// <summary>
		/// Machine's OnTriggerEnter will redirect call to current state's OnTriggerEnter()
		/// </summary>
		/// <param name="collider">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerEnter(Collider collider) { currentState.OnTriggerEnter(collider); }
		/// <summary>
		/// Machine's OnTriggerStay will redirect call to current state's OnTriggerStay()
		/// </summary>
		/// <param name="collider">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerStay(Collider collider) { currentState.OnTriggerStay(collider); }
		/// <summary>
		/// Machine's OnTriggerExit will redirect call to current state's OnTriggerExit()
		/// </summary>
		/// <param name="collider">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerExit(Collider collider) { currentState.OnTriggerExit(collider); }

		/// <summary>
		/// Machine's OnTriggerEnter2D will redirect call to current state's OnTriggerEnter2D()
		/// </summary>
		/// <param name="collider_2d">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerEnter2D(Collider2D collider_2d) { currentState.OnTriggerEnter2D(collider_2d); }
		/// <summary>
		/// Machine's OnTriggerStay2D will redirect call to current state's OnTriggerStay2D()
		/// </summary>
		/// <param name="collider_2d">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerStay2D(Collider2D collider_2d) { currentState.OnTriggerStay2D(collider_2d); }
		/// <summary>
		/// Machine's OnTriggerExit2D will redirect call to current state's OnTriggerExit2D()
		/// </summary>
		/// <param name="collider_2d">the other object's collider that caused the Trigger event</param>
		public virtual void OnTriggerExit2D(Collider2D collider_2d) { currentState.OnTriggerExit2D(collider_2d); }

		#endregion //collission and triggers

		public void SetInitialState<T>() where T : AbsState { initialState = allStates[typeof(T)]; }
		
		public void SetInitialState(System.Type T) { initialState = allStates[T]; }

		public void ChangeState<T>() where T : AbsState { ChangeState(typeof(T)); }
		
		public void ChangeState(System.Type T)
		{
			if (null != nextState)
			{
				#if (SYSTEM_DEBUG)
					Debug.Log("<color=cyan>" + name + "</color>: Running code to change from <color=blue>" + currentState.ToString() + "</color> state to <color=green>" + nextState.ToString() + "</color> state\n but you tried to change the state to <color=green>" + allStates[T].ToString() + "</color> which led to this error");
				#endif // SYSTEM_DEBUG
				throw new System.Exception(name + " is already changing states, you must wait to call ChangeState()!\n");
			}

			try
			{
				nextState = allStates[T];
			}
			catch (System.Collections.Generic.KeyNotFoundException e)
			{
				throw new System.Exception("\n" + name + "State: " + T + ".ChangeState() cannot find the state in the machine!\tDid you add the state you are trying to change to?\n" + e.Message);
			}

			onExit = true;
		}


		public bool IsCurrentState<T>() where T : AbsState { return (currentState.GetType() == typeof(T)) ? true : false; }
		public bool IsCurrentState(System.Type T) { return (currentState.GetType() == T) ? true : false; }

		public void AddState<T>() where T : AbsState, new()
		{
			if (!ContainsState<T>())
			{
				AbsState item = new T();
				item.machine = this;

				allStates.Add(typeof(T), item);
			}
		}
		public void AddState(System.Type T)
		{
			if (!ContainsState(T))
			{
				AbsState item = (AbsState)System.Activator.CreateInstance(T);
				item.machine = this;

				allStates.Add(T, item);
			}
		}

		public void RemoveState<T>() where T : AbsState { allStates.Remove(typeof(T)); }
		public void RemoveState(System.Type T) { allStates.Remove(T); }

		public bool ContainsState<T>() where T : AbsState { return allStates.ContainsKey(typeof(T)); }
		public bool ContainsState(System.Type T) { return allStates.ContainsKey(T); }

		public void RemoveAllStates() { allStates.Clear(); }

		public T CurrentState<T>() where T : AbsState { return (T)currentState; }

		public T PreviousState<T>() where T : AbsState { return (T)prevState; }

		public T GetState<T>() where T : AbsState { return (T)allStates[typeof(T)]; }
	}
}
