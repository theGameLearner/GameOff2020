using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.Singletons;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class GameManager : GenericSingletonMonobehaviour<GameManager>
{
    #region variables
        [Header("UI")]
        [SerializeField] GameObject GameOverPanel;
        [SerializeField] GameObject LevelCompletePanel;
        [SerializeField] GameObject SavePanel;
        [SerializeField] Text HeadingText;
        [SerializeField] Button saveButton;
        [SerializeField] Button LevelEditorButton;

        [SerializeField] InputField SaveFileNameInput;

        [SerializeField] Text SaveErrorText;

        [Header("save and load")]
        [SerializeField] GameObject LevelEditorparent;
        [SerializeField] GameObject GamePlayParent;
        int noOfEnemies;
        string FilepathToLoad;

        GameStates currGameState;
        string currentLevelName;
        bool testingLevel;

        string[] RestrictedLevelNames = {"saveFile","Level1"};
 
        
    #endregion

    #region Getters/setters
        public GameStates CurrGameState{
            get{
                return currGameState;
            }
            set{
                currGameState = value;
                InitializeState(value);
            }
        }
    #endregion

    #region Monobehaviour
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            int mode = PlayerPrefs.GetInt("GameStartState",0);
            if(mode == 1){
                string loadFileName = PlayerPrefs.GetString("FileToLoad",GameSettings.instance.defaultFileName);
                if(loadFileName == GameSettings.instance.defaultFileName){
                    testingLevel = true;
                }
                currentLevelName = loadFileName;
                LevelEditorManager.instance.LoadFromfile(Application.streamingAssetsPath+'/'+loadFileName+".json");
                CurrGameState = GameStates.GamePlay;
            }
            else{
                if(File.Exists(Application.streamingAssetsPath+'/'+GameSettings.instance.defaultFileName+".json")){
                    LevelEditorManager.instance.LoadFromfile(Application.streamingAssetsPath+'/'+GameSettings.instance.defaultFileName+".json");
                }
                CurrGameState = GameStates.LevelEditor;
            }
        }
    #endregion

    #region Private Methods
        private void InitializeState(GameStates state)
        {
            if(state == GameStates.LevelEditor){
                LevelEditorparent.SetActive(true);
                GamePlayParent.SetActive(false);
            }
            else if(state == GameStates.GamePlay){
                LevelEditorparent.SetActive(false);
                GamePlayParent.SetActive(true);
                GameSettings.instance.playerTransform.position = GameSettings.instance.playerSpawnSpotTransform.position + Vector3.up*0f;
                noOfEnemies = GameSettings.instance.NoOfEnemies;
            }
        }

        bool CheckIfFileNameAllowed(string fileName){

            Regex r = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
            if(!r.IsMatch(fileName)){
                return false;
            }

            foreach (string name in RestrictedLevelNames)
            {
                if(name == fileName){
                    return false;
                }
            }

            if(fileName == GameSettings.instance.defaultFileName){
                return false;
            }

            DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath);
            FileInfo[] Files = d.GetFiles("*.json");
            foreach(FileInfo file in Files )
            {
                if(fileName+".json" == file.Name){
                    return false;
                }
            }
            return true;
        }

        private void UploadToServer(string fileName)
        {
            string path = Application.streamingAssetsPath + '/' + fileName + ".json";
            string data = File.ReadAllText(path);
            LevelDetails levelDetails = new LevelDetails(fileName,data);
            DBCommunicator.StoreLevel(levelDetails,LevelSavedcallback);
        }

        void LevelSavedcallback(){
            Debug.Log("level saved");
        }

    #endregion

    #region public methods
        #region buttons
            public void TestLevel(){
                if(!LevelEditorManager.instance.IsLevelPlayable()){
                    return;
                }
                testingLevel = true;
                LevelEditorManager.instance.SaveLevel();
                LevelEditorManager.instance.LoadFromfile();
                currentLevelName = GameSettings.instance.defaultFileName;
                CurrGameState = GameStates.GamePlay;
            }

            public void Retry(){
                PlayerPrefs.SetInt("GameStartState",1);// 0 for level editor , 1 for play level
                PlayerPrefs.SetString("FileToLoad",currentLevelName);
                SceneManager.LoadScene(1);
            }

            public void SaveLevelOnComplete(){

                string fileName = SaveFileNameInput.text;
                if(fileName!="" && CheckIfFileNameAllowed(fileName)){
                    LevelEditorManager.instance.SaveLevel(fileName);
                    UploadToServer(fileName);
                    //TODO: show confirmation of save
                    MainMenu();
                }
                else{
                    SaveErrorText.text = "Invalid file name or file already exist";
                }
            }
            public void SaveButton(){
                SavePanel.SetActive(true);
            }

            public void MainMenu(){
                SceneManager.LoadScene(0);
            }
            public void LevelEditorBtn(){
                LevelEditorManager.instance.LoadFromfile(Application.streamingAssetsPath+'/'+GameSettings.instance.defaultFileName + ".json");
                CurrGameState = GameStates.LevelEditor;
            }
        #endregion

        public void GameOver()
	    {
            Debug.Log("Game Over");
            GameSettings.instance.playerTransform.gameObject.SetActive(false);
            GameOverPanel.SetActive(true);
            if(testingLevel){
                LevelEditorButton.gameObject.SetActive(true);
            }
            else{
                LevelEditorButton.gameObject.SetActive(false);
            }
            saveButton.gameObject.SetActive(false);
	    }

        public void LevelFinished(){
            // TODO: play cutscene
            GameSettings.instance.playerTransform.gameObject.SetActive(false);
            GameOverPanel.SetActive(true);
            HeadingText.text = "Congratulations !!\n Level Complete";
            if(testingLevel){
                LevelEditorButton.gameObject.SetActive(true);
                saveButton.gameObject.SetActive(true);
            }
            else{
                LevelEditorButton.gameObject.SetActive(false);
                saveButton.gameObject.SetActive(false);
            }

        }

        public void EnemyDestroyed(){
            noOfEnemies--;
            if(noOfEnemies<=0){
                LevelFinished();
            }
        }
    #endregion

}
