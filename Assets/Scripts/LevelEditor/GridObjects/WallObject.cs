﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class WallObject : MonoBehaviour, IGridObject
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
            WallData data = new WallData();
            return JsonConvert.SerializeObject(data);
        }

        public void GetXY(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public void Initialize(string jsonData)
        {
            WallData data = JsonConvert.DeserializeObject<WallData>(jsonData);
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
}

[System.Serializable]
public class WallData{

}
