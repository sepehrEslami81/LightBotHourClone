using Presenter.Level;
using Presenter.Procedure;
using Presenter.Robot;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Presenter.Ui
{
    public class ButtonsUiPresenter : MonoBehaviour
    {
        [Header("Script references")]
        [SerializeField] private RobotPresenter robotPresenter;
        [SerializeField] private TileMapPresenter tileMapPresenter;
        [SerializeField] private ProcedurePresenter procedurePresenter;
        [SerializeField] private CompleteLevelUiPresenter levelUiPresenter;
        
        [Header("run/stop button references")]
        [SerializeField] private Sprite runButtonSprite;
        [SerializeField] private Sprite stopButtonSprite;
        [SerializeField] private Image runStopButtonImage;

        private bool _isRunning;
        
        /// <summary>
        /// Button click listener: start or stop execute main procedure
        /// </summary>
        public void RunOrStopButtonClickEvent()
        {
            _isRunning = !_isRunning;
            
            if(_isRunning)
                ResetAndRun();
            else
                Stop();
        }

        /// <summary>
        /// Button click listener: go to levels scene
        /// </summary>
        public void ShowLevelsMenu()
        {
            SceneManager.LoadSceneAsync("LevelsMenu");
        }

        /// <summary>
        /// reset level and run main procedure
        /// </summary>
        private void ResetAndRun()
        {
            ResetLightCubes();
            
            robotPresenter.ResetRobot();
            procedurePresenter.StartProgram();
            
            runStopButtonImage.sprite = stopButtonSprite;
        }

        /// <summary>
        /// turn off all cube tile lights
        /// </summary>
        private void ResetLightCubes()
        {
            LevelPresenter.CountOfTurnedOnLightCubes = 0;
            
            levelUiPresenter.ChangePanelActiveStatus(false);
            tileMapPresenter.ResetAllLightCubes();
        }
        
        
        /// <summary>
        /// stop execute commands in main procedure
        /// </summary>
        private void Stop()
        {
            procedurePresenter.StopProgram();
            runStopButtonImage.sprite = runButtonSprite;
        }
    }
}