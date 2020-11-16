using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSpot : MonoBehaviour, IGridObject
{
    #region Variables
        int index;
        int x;
        int y;
    #endregion
    #region Interface
        public int GetIndex()
        {
            return index;
        }

        public string GetJsonData()
        {
            return "";
        }

        public void GetXY(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public void Initialize(string jsonData)
        {
            GameSettings.instance.playerSpawnSpotTransform = transform;   
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    #endregion

    #region monobehaviour
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void start()
        {
            GameSettings.instance.playerSpawnSpotTransform = transform;   
        }
    #endregion
}
