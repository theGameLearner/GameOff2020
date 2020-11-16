using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TGL.Singletons;
using System.IO;
using Newtonsoft.Json;

public class LevelEditorManager : GenericSingletonMonobehaviour<LevelEditorManager>
{
    #region variables
        [SerializeField] Camera camera;
        [SerializeField] GameObject baseSquare;

        [SerializeField] Transform LevelParent;
        [SerializeField] Transform LevelObjectsParent;
        [SerializeField] float cellSize = 5;

        List<GameObject> activeGridObjects;

        [HideInInspector]
        public GridObject selectedGridObject;
        public PlayerSpawnSpot playerSpawnSpot = null;

    #endregion

  

    #region Monobehavior
    // Start is called before the first frame update
    void Start()
        {
            DontDestroyOnLoad(this);
            GenerateNewGrid(10,10);
            activeGridObjects = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            //left click place object
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && GameManager.instance.CurrGameState == GameStates.LevelEditor){
                int x; int y;
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                Vector3 worldPos =  camera.ScreenToWorldPoint(mousePos);
                Debug.Log("world pos from camera = "+worldPos);
                if(GameSettings.instance.levelGrid.GetXY(worldPos,out x,out y))
                {
                    SpawnGridObject(x, y,selectedGridObject.index);
                }

            }   
        }

    

   
    #endregion

    #region private methods
        private IGridObject SpawnGridObject(int x, int y,int index)
        {
            GridObject objectToPlace = GameSettings.instance.gameData.GetGridObject(index);
            Vector3 pos = GameSettings.instance.levelGrid.GetGridPosition(x, y);
            GameObject existingGridObject;
            if (TryGetGridObjectAtposition(x, y, out existingGridObject))
            {
                activeGridObjects.Remove(existingGridObject);
                Destroy(existingGridObject);
            }
            GameObject go = Instantiate(objectToPlace.prefab, pos, Quaternion.identity, LevelObjectsParent);
            IGridObject gridObject = go.GetComponent<IGridObject>();
            gridObject.SetIndex(objectToPlace.index);
            gridObject.SetXY(x,y);
            
            //handle player Spawn spot
            if(gridObject.GetType() == typeof(PlayerSpawnSpot)){
                if(playerSpawnSpot!=null){
                    int _x,_y;
                    playerSpawnSpot.GetXY(out _x,out _y);
                    SpawnGridObject(_x,_y,0);
                }
                playerSpawnSpot = go.GetComponent<PlayerSpawnSpot>();
                GameSettings.instance.playerSpawnSpotTransform = go.transform;
            }

            go.name += "_" + x + "_" + y;
            activeGridObjects.Add(go);

            return go.GetComponent<IGridObject>();
        }

        void ClearGrid()
        {
            int j = LevelParent.childCount;
            for (int i = 0; i < j; i++)
            {
                Destroy(LevelParent.GetChild(i).gameObject);
            }
            foreach (GameObject Go in activeGridObjects)
            {
                Destroy(Go);
            }
            activeGridObjects.Clear();  
        }

        void GenerateNewGrid(int width,int height)
        {
            GameSettings.instance.levelGrid = new Grid<GridSquare>(width,height,cellSize,Vector3.zero);
            for (int x = 0; x < GameSettings.instance.levelGrid.Width; x++)
            {
                for (int y = 0; y < GameSettings.instance.levelGrid.Height; y++)
                {
                    GameObject go = new GameObject("gridBlock");
                    go.transform.SetParent(LevelParent,true);
                    go.transform.position = GameSettings.instance.levelGrid.GetGridPosition(x,y);
                    GridSquare gs = go.AddComponent<GridSquare>();
                    gs.baseSquare = baseSquare;
                    GameSettings.instance.levelGrid.gridArray[x,y] = gs;
                }
            }

            //set camera limits and teleport it back to middle;
            camera.GetComponent<EditorCameraControl>().SetLimits(width*cellSize,0,height*cellSize,0);
            camera.transform.position = new Vector3((width*0.5f*cellSize),camera.transform.position.y,width*0.5f*cellSize);
        }

        void SaveFile(string json, string path){
            
            File.WriteAllText(path, json);

        }
        
    #endregion

    #region public methods
        public void SaveGame(){
            
            SaveData saveData = new SaveData();
            saveData.gridWidth = GameSettings.instance.levelGrid.Width;
            saveData.gridHeight = GameSettings.instance.levelGrid.Height;

            saveData.objectList = new List<GridObjectSaveData>();

            foreach (GameObject Go in activeGridObjects)
            {
                GridObjectSaveData gridObjectSaveData = new GridObjectSaveData();
                IGridObject gridObject = Go.GetComponent<IGridObject>();
                int _x;int _y;
                GameSettings.instance.levelGrid.GetXY(Go.transform.position,out _x,out _y);
                gridObjectSaveData.x = _x;
                gridObjectSaveData.y = _y;
                gridObjectSaveData.index = gridObject.GetIndex();
                gridObjectSaveData.objectData = gridObject.GetJsonData();

                saveData.objectList.Add(gridObjectSaveData);
            }
            
            string json = JsonConvert.SerializeObject(saveData);
            Debug.Log(json);
            SaveFile(json,Application.streamingAssetsPath+"/saveFile.json");
           

        }

        public void load(string json){

            SaveData loadData = JsonConvert.DeserializeObject<SaveData>(json);
            ClearGrid();
            GenerateNewGrid(loadData.gridWidth,loadData.gridHeight);

            foreach (GridObjectSaveData objectData in loadData.objectList)
            {
                IGridObject spawnedObject = SpawnGridObject(objectData.x,objectData.y,objectData.index);
                spawnedObject.Initialize(objectData.objectData);
            }
        }

        public void LoadFromfile(string path){
            string json = "";
            if(System.IO.File.Exists(path)){
                json = System.IO.File.ReadAllText(path);
                load(json);
            }
            else{
                Debug.LogError("no such file or folder : "+path);
            }

            
        }

    
        public void createGrid(int width,int height)
        {
            ClearGrid();
            GenerateNewGrid(width,height);
        }

        public bool TryGetGridObjectAtposition(int x,int y,out GameObject gridObject){
            gridObject = null;
            int _x; int _y;
            for (int i = 0; i < activeGridObjects.Count; i++)
            {
                GameSettings.instance.levelGrid.GetXY(activeGridObjects[i].transform.position,out _x,out _y);
                if(_x == x && _y == y){
                    gridObject = activeGridObjects[i];
                    return true;
                }
            }
            return false;
        }

    #endregion
}
[System.Serializable]
public struct SaveData{
    public int gridWidth;
    public int gridHeight;
    public List<GridObjectSaveData> objectList;
}

[System.Serializable]
public struct GridObjectSaveData{
    public int index;

    public int x;

    public int y;
    
    public string objectData;
}
