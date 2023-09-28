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
        [SerializeField] private float lerpTime = 0.1f;

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
            var tile = TileMapPresenter.StartTile;

            var wp = tile.WorldPosition;
            wp.y = _robotModel.RobotHeight + tile.Height;

            _robotModel.Position = tile.Position;
            _robotModel.RobotGameObject.transform.position = wp;
        }

        public Position GetNextTilePosByDirection()
        {
            return _robotModel.Direction switch
            {
                RobotDirection.Forward => new Position(0, 1),
                RobotDirection.Backward => new Position(0, -1),
                RobotDirection.Left => new Position(-1, 0),
                RobotDirection.Right => new Position(1, 0),

                // just for fix rider warning
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IEnumerator Move(Position newPosition, Vector3 newWorldPosition, int tileHeight,
            double positionThreshold)
        {
            var targetWorldPos = CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight);
            
            while ((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude > positionThreshold)
            {
                print((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude);

                var a = _robotModel.CurrentWorldPosition;
                LerpPosition(a, targetWorldPos, lerpTime);

                yield return new WaitForFixedUpdate();
            }

            FixPosition(targetWorldPos, newPosition);
            yield return new WaitForFixedUpdate();

            print("done");
            
            
            
            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }
        
        public IEnumerator Jump(Position newPosition, Vector3 newWorldPosition, int tileHeight,
            double positionThreshold)
        {
            var targetWorldPos = CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight);
            
            while ((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude < positionThreshold)
            {
                print((_robotModel.RobotGameObject.transform.position - newWorldPosition).magnitude);

                var a = _robotModel.CurrentWorldPosition;
                LerpPosition(a, targetWorldPos, lerpTime);

                yield return new WaitForFixedUpdate();
            }

            FixPosition(targetWorldPos, newPosition);
            yield return new WaitForFixedUpdate();

            print("done");
            
            
            
            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }


        private void LerpPosition(Vector3 a, Vector3 b, float t)
        {
            var lerp = Vector3.Lerp(a, b, t);
            _robotModel.RobotGameObject.transform.position = lerp;
        }

        private void FixPosition(Vector3 targetWorldPos, Position targetPos)
        {
            _robotModel.Position = targetPos;
            _robotModel.RobotGameObject.transform.position = targetWorldPos;
        }

    }
}