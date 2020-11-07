using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    #region variables
        int height;
        int width;
        float cellSize;

        [HideInInspector]
        public TGridObject[,] gridArray;
        Vector3 origin;
    #endregion

    #region getters
        public int Height{
            get
            {
                return height;
            }
        }
        public int Width{
            get
            {
                return width;
            }
        }
        public Vector3 Origin{
            get
            {
                return origin;
            }
        }
    #endregion

    #region Constructors
        public Grid(int height, int width, float cellSize, Vector3 origin)
        {
            this.height = height;
            this.width = width;
            this.cellSize = cellSize;
            this.origin = origin;

            gridArray = new TGridObject[width,height];
            //visual representation of grid for debug
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 rayOrigin = new Vector3(origin.x+x*cellSize,origin.y,origin.y+y*cellSize); 
                    Debug.DrawRay(rayOrigin,Vector3.right*cellSize,Color.white,float.MaxValue);
                    Debug.DrawRay(rayOrigin,Vector3.forward*cellSize,Color.white,float.MaxValue);
                }
                Debug.DrawRay(new Vector3(origin.x+width*cellSize,origin.y,origin.y+height*cellSize),Vector3.left*cellSize*width,Color.white,float.MaxValue);
                Debug.DrawRay(new Vector3(origin.x+height*cellSize,origin.y,origin.y+height*cellSize),Vector3.back*cellSize*height,Color.white,float.MaxValue);
            }
        }
    #endregion

    #region public methods
        public bool GetXY(Vector3 postion,out int x,out int y){
            x = Mathf.FloorToInt(postion.x-origin.x/cellSize);
            y = Mathf.FloorToInt(postion.y-origin.y/cellSize);
            if(x >= 0 && x < width && y >= 0 && y < height){
                return true;
            }
            else{
                return false;
            } 
        }

        public Vector3 GetGridPosition(int x,int y){
            if(x >= 0 && x < width && y >= 0 && y < height){
                return new Vector3(origin.x + x*cellSize + cellSize/2,origin.y,origin.z + y*cellSize + cellSize/2);
            }
            else{
                return origin;
            }
        }
    #endregion
}
