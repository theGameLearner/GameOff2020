using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class WallObject : MonoBehaviour, IGridObject
{
    #region Variables
        int index;
    #endregion
    #region Interface
        public int GetIndex()
        {
            return index;
        }

        public string GetJsonData()
        {
            WallData data = new WallData();
            data.a = 1;
            data.b ="abc";
            data.c = Vector3.zero;

            return JsonConvert.SerializeObject(data);
        }

        public void GetXY(out int x, out int y)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(string jsonData)
        {
            WallData data = JsonConvert.DeserializeObject<WallData>(jsonData);
            Debug.Log(data.a);
            Debug.Log(data.b);
            Debug.Log(data.c);
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public void SetXY(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    #endregion
}

[System.Serializable]
public class WallData{
    public int a;
    public string b;
    public Vector3 c;
}

