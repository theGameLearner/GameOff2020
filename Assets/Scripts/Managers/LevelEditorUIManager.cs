using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorUIManager : MonoBehaviour
{
    #region Variables
        [SerializeField] InputField GridLength;
        [SerializeField] InputField GridWidth;

        [SerializeField] InputField LoadDataField;

        [SerializeField] Text messagetext;

        [SerializeField] Dropdown objectDropDown;
        
    #endregion

    #region getters
    #endregion

    #region monobehaviour
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
            foreach (GridObject gridObject in GameSettings.instance.gameData.AvailableGridObjects)
            {
                Dropdown.OptionData optionData = new Dropdown.OptionData(gridObject.name);
                optionDatas.Add(optionData);
            }
            objectDropDown.ClearOptions();
            objectDropDown.AddOptions(optionDatas);
            objectDropDown.value = 0;
            OnObjectvalueChanged(objectDropDown);

        }      
    #endregion

    #region privateMethods
        void displayMessage(string message){
            messagetext.text = message;
        }
    #endregion

    #region Public methods
        #region buttons
            public void createGrid(){
                int width;
                int length;
                if(int.TryParse(GridWidth.text,out width)){
                    if(int.TryParse(GridLength.text,out length)){
                        if(width>0 && length>0){
                            LevelEditorManager.instance.createGrid(width,length);
                            displayMessage("grid created");
                            return;
                        }
                    }
                }
                displayMessage("invalid length or width");
            }

            public void Loadtest(){
                LevelEditorManager.instance.load(LoadDataField.text);
            }
        #endregion
    #endregion

    #region eventHandlers
        public void OnObjectvalueChanged(Dropdown dropdown){
            string objectName = dropdown.options[dropdown.value].text;
            GridObject gridObject = GameSettings.instance.gameData.AvailableGridObjects.Find((o => o.name == objectName));
            if(gridObject != null){
                LevelEditorManager.instance.selectedGridObject = gridObject;
            }
        }
    #endregion
}
