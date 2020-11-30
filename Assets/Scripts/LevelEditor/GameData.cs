using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{

    public List<string> CampaignLevelNames;
    public List<GridObject> AvailableGridObjects;
    public GridObject defaultGridObject;

    public GridObject GetGridObject(int index){
        GridObject objectToReturn = AvailableGridObjects.Find( x => x.index == index);
        if(objectToReturn == null){
            return defaultGridObject;
        }
        return objectToReturn;       
    }
   
}

[System.Serializable]
public class GridObject
{
    public string name;
    public int index;
    public GameObject prefab;
}
