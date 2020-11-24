using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region variables
        [SerializeField] GameObject MenuCanvas;
        [SerializeField] GameObject FileCanvas;

        [SerializeField] GameObject FileButtonContainer;
        [SerializeField] GameObject FileButtonPrefab;
    #endregion

    #region MonoBehaviour
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            MenuCanvas.SetActive(true);
            FileCanvas.SetActive(false);
        }
    #endregion

    #region Public methods

        public void FetchOnlineFiles(){
            DBCommunicator.GetAllLevels(GetOnlineLevelsCallback);
        }
        public void LoadlevelButton(){
            FileCanvas.SetActive(true);
            MenuCanvas.SetActive(false);
            FetchFiles();
        }

        public void BackButton(){
            MenuCanvas.SetActive(true);
            FileCanvas.SetActive(false);
        }
        public void LeveleditorButton(){
            PlayerPrefs.SetInt("GameStartState",0);// 0 for level editor , 1 for play level
            SceneManager.LoadScene(1);
        }
    #endregion
    #region Private methods
        void FetchFiles(){
            DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath);
            FileInfo[] Files = d.GetFiles("*.json");
            foreach(FileInfo file in Files )
            {
                GameObject buttonGo = Instantiate(FileButtonPrefab,FileButtonContainer.transform.position,Quaternion.identity,FileButtonContainer.transform);
                Button button = buttonGo.GetComponent<Button>();
                button.onClick.AddListener(() => {LoadLevel(file.Name);});
                button.GetComponentInChildren<Text>().text = file.Name;
            }
        }

        void LoadLevel(string levelFileName){
            PlayerPrefs.SetInt("GameStartState",1);// 0 for level editor , 1 for play level
            PlayerPrefs.SetString("FileToLoad",levelFileName.Split('.')[0]);
            SceneManager.LoadScene(1);
        }

        void GetOnlineLevelsCallback(List<LevelDetails> levels){

            foreach (LevelDetails level in levels)
            {
                string data = level.levelJson;
                string path = Application.streamingAssetsPath + '/' + level.levelName + ".json";
                if(File.Exists(path)){
                    File.Delete(path);
                }
                File.WriteAllText(path,data);
            }
        }   
    #endregion
}
