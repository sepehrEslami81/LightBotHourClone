using Model.Level;
using Model.Robot;
using UnityEngine;

namespace Presenter.Robot
{
    public class RobotPresenter : MonoBehaviour
    {
        [SerializeField] private Transform robotObject;
        [SerializeField] private float robotHeight;
        
        private RobotModel _robotModel;
        private LevelModel _currentLevel; 

        private void Awake()
        {
            _robotModel = new RobotModel()
            {
                RobotHeight = robotHeight,
                RobotGameObject = robotObject ? robotObject : transform 
            };
        }

        public void SetCurrentLevel(LevelModel model)
        {
            _currentLevel = model;
        }
        
        public void SetStartPosition(Vector3 pos)
        {
            pos.y += _robotModel.RobotHeight;
            _robotModel.StartPosition = pos;
        }

        public void ResetRobotPosition()
        {
            _robotModel.RobotGameObject.position = _robotModel.StartPosition;
        }
        
        
    }
}