using Presenter.Level;
using Presenter.Procedure;
using Presenter.Robot;
using UnityEngine;
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
        
        public void RunOrStopButtonClickEvent()
        {
            _isRunning = !_isRunning;
            
            if(_isRunning)
                ResetAndRun();
            else
                Stop();
        }

        private void ResetAndRun()
        {
            ResetLightCubes();
            
            robotPresenter.ResetRobot();
            procedurePresenter.StartProgram();
            
            runStopButtonImage.sprite = stopButtonSprite;
        }

        private void ResetLightCubes()
        {
            levelUiPresenter.CountOfTurnedOnLightCubes = 0;
            levelUiPresenter.HidePanel();
            tileMapPresenter.ResetAllLightCubes();
        }
        
        private void Stop()
        {
            procedurePresenter.StopProgram();
            runStopButtonImage.sprite = runButtonSprite;
        }
    }
}