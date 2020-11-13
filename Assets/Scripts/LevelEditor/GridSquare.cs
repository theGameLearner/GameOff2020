using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    #region variables
        public GameObject baseSquare;
    #endregion
    
    #region monobehaviour
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            SpawnBase();
        }
    #endregion
    #region private methods
        void SpawnBase(){
            Instantiate(baseSquare,transform.position,Quaternion.identity,transform);
        }
    #endregion
}
