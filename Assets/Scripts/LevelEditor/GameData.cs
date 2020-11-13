using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{

    public List<GridObject> AvailableGridObjects;
    public GridObject defaultGridObject;
   
}

[System.Serializable]
public class GridObject
{
    public string name;
    public int index;
    public GameObject prefab;
}
