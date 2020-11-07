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

public class DummyMachine : AbsStateMachine
{
    public static DummyMachine _instance;

    /// <summary>
    /// to be used in inspector for debugging faster. Just a hint of alternative idea to get current state.
    /// </summary>
    public string stateName;

    /// <summary>
    /// create a instance for the machine that can be fetched by a state
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #if TGL_DEBUG
            Debug.Log("01. First Line executed");
        #endif
    }

    /// <summary>
    /// Called by base.Start() so remember to have SetInitialState here to avoid errors
    /// use AddStates to add all states the code can be a part of, and set the initial state here
    /// </summary>
    public override void AddStates()
    {
        AddState<DummyState>();
        AddState<DummyState02>();
        SetInitialState<DummyState>();

        #if TGL_DEBUG
            Debug.Log("03. Third Line executed");
        #endif
    }

    /// <summary>
    /// Flow of code can be seen in comments below
    /// </summary>
    public override void Start()
    {
        #if TGL_DEBUG
            Debug.Log("02. Second Line executed");
        #endif
        base.Start();
        #if TGL_DEBUG
            Debug.Log("04. Fourth Line executed");
        #endif
    }

    /// <summary>
    /// States can be added as needed and changed, or you can add them before hand and change to them
    /// </summary>
    public void ChangeStateToNew()
	{
        //AddState<DummyState02>();
        ChangeState<DummyState02>();
    }

    public void ChangeStateToOld()
    {
        ChangeState<DummyState>();
    }
}
