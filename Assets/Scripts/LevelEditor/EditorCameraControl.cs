using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EditorCameraControl : MonoBehaviour
{
    #region variables
        
        float xMinLimit;
        float xMaxLimit;
        float yMinLimit;
        float yMaxLimit;

        Camera camera;
        [SerializeField] float minOrthographicSize = 20f;

        [SerializeField] float zoomRate = 1f;

        [SerializeField] float maxOrthographicSize = 40f;
        [SerializeField] float LimitTolerance = 2f;

        [SerializeField] float moveSpeed = 5;

    #endregion

    #region monobehaviour

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            camera = GetComponent<Camera>();
            camera.orthographicSize = maxOrthographicSize;
        }
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            //zoom out
            if(Input.mouseScrollDelta.y < 0){
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize + zoomRate,minOrthographicSize,maxOrthographicSize);
            }
            
            //zoom in
            if(Input.mouseScrollDelta.y > 0){
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomRate,minOrthographicSize,maxOrthographicSize);
            }

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
