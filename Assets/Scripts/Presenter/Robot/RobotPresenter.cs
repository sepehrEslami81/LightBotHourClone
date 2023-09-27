using System;
using System.Collections;
using Model;
using Model.Robot;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Robot
{
    
    public class RobotPresenter : MonoBehaviour
    {
        [SerializeField] private Transform robotObject;
        [SerializeField] private float robotHeight;
        [SerializeField] private float lerpTime = 0.1f;
        [SerializeField] private float positionThreshold = 1.5f;

        private RobotModel _robotModel;
        private LevelPresenter _levelPresenter;
        
        private void Awake()
        {
            _robotModel = new RobotModel()
            {
                RobotHeight = robotHeight,
                RobotGameObject = robotObject ? robotObject : transform 
            };
        }

        public void SetLevelPresenter(LevelPresenter level)
        {
            _levelPresenter = level;
        }

        public void ResetRobotPosition()
        {
            var tile = _levelPresenter.StartTile;
            var wp = tile.WorldPosition;
            wp.y = _robotModel.RobotHeight + tile.Height;

            _robotModel.CurrentPosition = tile.Position;
            _robotModel.RobotGameObject.transform.position = wp;
        }

        public IEnumerator Walk(Position newPosition, Vector3 newWorldPosition, int tileHeight) {
            while ((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude > positionThreshold)
            {
                
                var lerp = Vector3.Lerp(_robotModel.CurrentWorldPosition, CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight), lerpTime);
                _robotModel.RobotGameObject.transform.position = lerp;
                yield return new WaitForFixedUpdate();
            }
            
            
            _robotModel.CurrentPosition = newPosition;
            _robotModel.RobotGameObject.transform.position = CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight);
            yield return new WaitForFixedUpdate();


            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }
    }
}