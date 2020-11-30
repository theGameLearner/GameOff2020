using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.Singletons;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using Cinemachine;

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

        [Header("camera")]
        [SerializeField] CinemachineVirtualCamera virtualCamera;
 
        
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
                Debug.Log(loadFileName);
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
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
            }
            else if(state == GameStates.GamePlay)
        {
            LevelEditorparent.SetActive(false);
            GamePlayParent.SetActive(true);
            StartGame();
        }
    }

    private void StartGame()
    {
        Transform playerTransform = GameSettings.instance.playerTransform;
        playerTransform.gameObject.SetActive(true);
        playerTransform.position = GameSettings.instance.playerSpawnSpotTransform.position + Vector3.up * 0f;
        MovementInput movementInput = playerTransform.GetComponent<MovementInput>();
        movementInput.playerAcceptsInput = true;
        playerTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        noOfEnemies = GameSettings.instance.NoOfEnemies;
        virtualCamera.m_Follow = playerTransform;
        virtualCamera.m_LookAt = playerTransform;
        GameOverPanel.SetActive(false);
        SavePanel.SetActive(false);
    }

    bool CheckIfFileNameAllowed(string fileName){

            Regex r = new Regex("^[a-zA-Z][a-zA-Z0-9]*$");
            if(!r.IsMatch(fileName)){
                return false;
            }

            foreach (string name in GameSettings.instance.gameData.CampaignLevelNames)
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
                    if(File.Exists(Application.streamingAssetsPath+'/'+GameSettings.instance.defaultFileName + ".json")){
                        File.Delete(Application.streamingAssetsPath+'/'+GameSettings.instance.defaultFileName + ".json");
                    }
                    MainMenu();
                }
                else{
                    SaveErrorText.text = "Invalid file name or file already exist";
                }
            }
            public void SaveButton(){
                SavePanel.SetActive(true);
                GameOverPanel.SetActive(false);
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
            if(CurrGameState == GameStates.GameOver){
                return;
            }
            Debug.Log("Game Over");
            AudioManager.instance.PlayplayerDiedClip();
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
            // GameSettings.instance.playerTransform.gameObject.SetActive(false);
            CurrGameState = GameStates.GameOver;
            virtualCamera.m_Follow = null;
            virtualCamera.m_LookAt = null; 
            MovementInput movementInput = GameSettings.instance.playerTransform.gameObject.GetComponent<MovementInput>();
            movementInput.playerAcceptsInput = false;
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
            AudioManager.instance.PlaykillTurretClip();
            if(noOfEnemies<=0){
                LevelFinished();
            }
        }
    #endregion

}
