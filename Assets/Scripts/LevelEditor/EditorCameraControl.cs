using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraControl : MonoBehaviour
{
    #region variables
        
        float xMinLimit;
        float xMaxLimit;
        float yMinLimit;
        float yMaxLimit;

        [SerializeField] float LimitTolerance = 2f;

        [SerializeField] float moveSpeed = 5;

    #endregion

    #region monobehaviour
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(Input.GetKey(KeyCode.W)){
                Vector3 newPos = transform.position;
                newPos += Vector3.forward * moveSpeed*Time.deltaTime;
                if(newPos.z > yMaxLimit+LimitTolerance){
                    newPos.z = yMaxLimit+LimitTolerance;
                }
                transform.position = newPos;
            }
            if(Input.GetKey(KeyCode.S)){
                Vector3 newPos = transform.position;
                newPos += Vector3.back * moveSpeed*Time.deltaTime;
                if(newPos.z < yMinLimit-LimitTolerance){
                    newPos.z = yMinLimit-LimitTolerance;
                }
                transform.position = newPos;
            }
            if(Input.GetKey(KeyCode.A)){
                Vector3 newPos = transform.position;
                newPos += Vector3.left * moveSpeed*Time.deltaTime;
                if(newPos.x < xMinLimit-LimitTolerance){
                    newPos.x = xMinLimit-LimitTolerance;
                }
                transform.position = newPos;
            }
            if(Input.GetKey(KeyCode.D)){
                Vector3 newPos = transform.position;
                newPos += Vector3.right * moveSpeed*Time.deltaTime;
                if(newPos.x > xMaxLimit+LimitTolerance){
                    newPos.x = xMaxLimit+LimitTolerance;
                }
                transform.position = newPos;
            }
        }
        
    #endregion


    #region public methods
        public void SetLimits(float xMaxLimit,float xMinLimit,float yMaxLimit,float yMinLimit){
            this.xMaxLimit = xMaxLimit;
            this.xMinLimit = xMinLimit;
            this.yMaxLimit = yMaxLimit;
            this.yMinLimit = yMinLimit;
        }
    #endregion
}
