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
        [SerializeField] private ProcedurePresenter procedurePresenter;
        
        [Header("run/stop button references")]
        [SerializeField] private Sprite runButtonSprite;
        [SerializeField] private Sprite stopButtonSprite;
        [SerializeField] private Image runStopButtonImage;

        private bool _isRunning;
        
        public void RunOrStopButtonClickEvent()
        {
            _isRunning = !_isRunning;
            
            if(_isRunning)
                Run();
            else
                Stop();
        }

        private void Run()
        {
            procedurePresenter.StartProgram();
            runStopButtonImage.sprite = stopButtonSprite;
        }
        
        private void Stop()
        {
            procedurePresenter.StopProgram();
            robotPresenter.ResetRobot();
            runStopButtonImage.sprite = runButtonSprite;
        }
    }
}