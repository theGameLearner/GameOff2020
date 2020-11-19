/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #25-Oct-2020#
 */


/*
 * Used internet link for reference, but the code is mae by TGL
 * ref : https://csharpindepth.com/articles/singleton
 */


using UnityEngine;


namespace TGL.Singletons
{

    //defination states that the generic class passed will also be derived from monobehaviour
    public class GenericSingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _genericInstance; //the instance that is created once and never repeated
        private static readonly object _lockObj = new object(); // a locking mechanism so that new instances cannot be created while lock is enabled

        public static T instance
        {
            get
            {
                lock (_lockObj)
                {
                    //Debug.Log("locked the instance for " + typeof(T));
                    if (_genericInstance == null)
                    {
                        //check if there are more than one instances in scene
                        object[] availableInstances = FindObjectsOfType(typeof(T));
                        if (availableInstances.Length == 1)
                        {
                            //there is one instance already set, but not assigned.
                            Debug.LogError("There was one instances of class " + typeof(T) + " already available in scene, but it was not assigned to our instancce variable");
                            _genericInstance = (T)availableInstances[0];
                            return _genericInstance;
                        }
                        else if (availableInstances.Length > 1)
                        {
                            //meaning there are more than one script instances in current scene
                            Debug.LogError("There are more than one instances of class " + typeof(T) + " already available in scene");
                            //return any random instance in the list
                            _genericInstance = (T)availableInstances[0];
                            return _genericInstance;
                        }
                        else
                        {
                            Debug.LogWarning("As no instance of singleton class " + typeof(T) + " exists in project, creating one here.");
                            //creating a game object on current scene hierarchy to have this singleton component
                            GameObject singletonObj = new GameObject("Singleton_" + typeof(T).ToString()); //game object in Hierarchy root
                            _genericInstance = singletonObj.AddComponent<T>(); // instance definition is set here so we don't have to find using type
                                                                               //dont destroy the new object as we might need it again
                            DontDestroyOnLoad(singletonObj);
                            Debug.LogWarning("the new instance of " + typeof(T) + " created by \'GenericSingletonMonobehaviour\' class is " + singletonObj);
                            return _genericInstance;
                        }

                    }
                    else
                    {
                        return _genericInstance;
                    }
                }
            }

        }

        public void Awake()
        {
            if (_genericInstance == null)
            {
                _genericInstance = this as T;
            }
            else if (_genericInstance != (this as T))
            {
                Debug.LogError("creating a new copy of existing singleton class with type as " + typeof(T) + " is not allowed in gameobject " + (this as T).gameObject);
                Destroy((this as T));
            }
        }
    }

}