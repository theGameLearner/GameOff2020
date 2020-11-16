using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.Singletons;
using System;

public class GameManager : GenericSingletonMonobehaviour<GameManager>
{
    #region variables
        [SerializeField] GameObject LevelEditorparent;
        [SerializeField] GameObject GamePlayParent;

        [SerializeField] string FilepathToLoad;

        GameStates currGameState;
        
    #endregion

    #region Getters/setters
        public GameStates CurrGameState{
            get{
                return currGameState;
            }
            set{
                currGameState = value;
                SwitchState(value);
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
            CurrGameState = GameStates.LevelEditor;
        }
    #endregion

    #region Private Methods
        private void SwitchState(GameStates state)
        {
            if(state == GameStates.LevelEditor){
                LevelEditorparent.SetActive(true);
                GamePlayParent.SetActive(false);
            }
            else if(state == GameStates.GamePlay){
                LevelEditorparent.SetActive(false);
                GamePlayParent.SetActive(true);
                GameSettings.instance.playerTransform.position = GameSettings.instance.playerSpawnSpotTransform.position;
            }
        }

    #endregion

    #region public methods
        public void TestLevel(){
            CurrGameState = GameStates.GamePlay;
        }

        public void GameOver()
	    {
            Debug.Log("Game Over");
	    }
    #endregion

}
