using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    #region variables
        [SerializeField] GridObjectDictionary gridObjectDictionary;
        [SerializeField] GameObject baseSquare;
        Grid<GridSquare> levelgrid;
        GridObjectType selectedGridObject;
    #endregion

    #region singleton
        private static LevelEditorManager _instance = null;
        public static LevelEditorManager Instance{
            get{
                if(_instance == null){
                    _instance = FindObjectOfType<LevelEditorManager>();
                }
                return _instance;
            }
        }
        void Awake(){
            if(_instance == null){
                _instance = this;
            }
            else if(_instance != this){
                Destroy(this.gameObject);
            }
        }        
    #endregion
    #region Monobehavior
        // Start is called before the first frame update
        void Start()
        {
            levelgrid = new Grid<GridSquare>(10,10,5,Vector3.zero);
            for (int x = 0; x < levelgrid.Width; x++)
            {
                for (int y = 0; y < levelgrid.Height; y++)
                {
                    GameObject go = new GameObject("gridBlock");
                    go.transform.position = levelgrid.GetGridPosition(x,y);
                    GridSquare gs = go.AddComponent<GridSquare>();
                    gs.baseSquare = baseSquare;
                    levelgrid.gridArray[x,y] = gs;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    #endregion

    #region private methods
        
    #endregion

    #region public methods
        public GameObject GetGridObjectPrefab(GridObjectType gridObjectType){
            GameObject gridObjectPrefab = null;
            if(!gridObjectDictionary.TryGetValue(gridObjectType,out gridObjectPrefab)){
                Debug.LogError("gridObject of type "+gridObjectType + "does not exist");
            }
            return gridObjectPrefab;
        }

        #region buttons
            public void createGrid(){

            }
        #endregion
    #endregion
}

[System.Serializable]
public class GridObjectDictionary : Dictionary<GridObjectType,GameObject> {}

public enum GridObjectType{
    WallBlock,
    DestructibleBlock,
    basicEnemyTurret,
    Empty
}
