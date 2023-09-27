using System.Collections;
using Model;
using Model.Robot;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Robot
{
    
    public class RobotPresenter : MonoBehaviour
    {
        [SerializeField] private float lerpTime = 0.1f;
        [SerializeField] private float positionThreshold = 1.5f; // based on the test result

        private RobotModel _robotModel;
        
        private void Awake()
        {
            if (TryGetComponent(out RobotModel model))
            {
                _robotModel = model;
            }
            else
            {
                Debug.LogError("failed to load robot model.");
            }
        }

        public void ResetRobotPosition()
        {
            var tile = LevelPresenter.StartTile;
            var wp = tile.WorldPosition;
            wp.y = _robotModel.RobotHeight + tile.Height;

            _robotModel.Position = tile.Position;
            _robotModel.RobotGameObject.transform.position = wp;
        }

        public IEnumerator Walk(Position newPosition, Vector3 newWorldPosition, int tileHeight) {
            while ((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude > positionThreshold)
            {
                
                var lerp = Vector3.Lerp(_robotModel.CurrentWorldPosition, CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight), lerpTime);
                _robotModel.RobotGameObject.transform.position = lerp;
                yield return new WaitForFixedUpdate();
            }
            
            
            _robotModel.Position = newPosition;
            _robotModel.RobotGameObject.transform.position = CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight);
            yield return new WaitForFixedUpdate();


            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }
    }
}