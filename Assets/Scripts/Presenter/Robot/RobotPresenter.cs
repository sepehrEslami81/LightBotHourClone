using System;
using System.Collections;
using Model;
using Model.Level;
using Model.Robot;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Robot
{
    public class RobotPresenter : MonoBehaviour
    {
        [Header("Lerp speed settings")] [Range(1, 100), SerializeField]
        private float lerpPosSpeed = 60;

        [Range(1, 360), SerializeField] private float lerpRotSpeed = 60;

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

        public Position GetNextTilePosByDirection()
        {
            return _robotModel.Direction switch
            {
                RobotDirection.Forward => new Position(0, 1),
                RobotDirection.Left => new Position(1, 0),
                RobotDirection.Backward => new Position(0, -1),
                RobotDirection.Right => new Position(-1, 0),

                // just for fix rider warning
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IEnumerator Move(Position newPosition, Vector3 newWorldPosition, int tileHeight)
        {
            var targetWorldPos = CreatePosition(newWorldPosition, tileHeight, _robotModel.RobotHeight);


            float t = 0f;
            float moveDuration = Mathf.Abs(90f / lerpPosSpeed);

            while (t < moveDuration)
            {
                var a = _robotModel.CurrentWorldPosition;
                LerpPosition(a, targetWorldPos, t / moveDuration);

                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            FixPosition(targetWorldPos, newPosition);
            yield return new WaitForFixedUpdate();


            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }

        public IEnumerator Rotate(RobotDirection direction)
        {
            Vector3 startRotation = _robotModel.RobotGameObject.rotation.eulerAngles;
            float targetRotationY = startRotation.y + DirectionToAngel(direction).y;
            Vector3 targetRotation = new Vector3(0, targetRotationY, 0);

            float t = 0f;
            float rotationDuration = Mathf.Abs(90f / lerpRotSpeed);

            while (t < rotationDuration)
            {
                EulerRotation(startRotation, targetRotation, t / rotationDuration);

                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            FixRotation(targetRotation, direction);
            yield return new WaitForFixedUpdate();
        }

        public void SetDefaultPos(Position pos)
        {
            _robotModel.StartPosition = pos;
        }

        public void SetDefaultDirection(RobotDirection dir)
        {
            _robotModel.StartDirection = dir;
        }

        public void ResetRobot()
        {
            ResetPosition();
            ResetRotation();
        }


        private void ResetRotation()
        {
            var angel = DirectionToAngel(_robotModel.StartDirection);
            FixRotation(angel, _robotModel.StartDirection);
            _robotModel.Direction = _robotModel.StartDirection;
        }

        private void ResetPosition()
        {
            var tile = TileMapPresenter.GetTileByPosition(_robotModel.StartPosition);
            FixPosition(tile.WorldPosition, tile.Position);
        }

        private void EulerRotation(Vector3 startRotation, Vector3 targetRotation, float rotationDuration)
        {
            Vector3 currentRotation = Vector3.Lerp(startRotation, targetRotation, rotationDuration);
            _robotModel.RobotGameObject.rotation = Quaternion.Euler(currentRotation);
        }


        private void LerpPosition(Vector3 a, Vector3 b, float t)
        {
            var lerp = Vector3.Lerp(a, b, t);
            _robotModel.RobotGameObject.transform.position = lerp;
        }


        private void FixPosition(Vector3 targetWorldPos, Position targetPos)
        {
            _robotModel.Position = targetPos;

            var currentTile = TileMapPresenter.GetTileByPosition(targetPos);

            var y = CalculateYAxis(currentTile);
            var worldPos = new Vector3(targetWorldPos.x, y, targetWorldPos.z);
            _robotModel.RobotGameObject.transform.position = worldPos;

            float CalculateYAxis(CubeTileModel model) => _robotModel.RobotHeight + model.Height;
        }

        private void FixRotation(Vector3 targetAngles, RobotDirection dir)
        {
            transform.eulerAngles = targetAngles;
            _robotModel.Direction = CalculateCurrentDirection(dir);

            RobotDirection CalculateCurrentDirection(RobotDirection rotedToDirection)
            {
                int directionNum = (int)rotedToDirection;
                return (RobotDirection)((int)(_robotModel.Direction + directionNum) % 4);
            }
        }
        
        
        private Vector3 DirectionToAngel(RobotDirection dir)
        {
            return dir switch
            {
                RobotDirection.Backward => new Vector3(0, 180, 0),
                RobotDirection.Forward => new Vector3(0, 0, 0),
                RobotDirection.Left => new Vector3(0, -90, 0),
                RobotDirection.Right => new Vector3(0, 90, 0),
                
                // remove rider warning 
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }

    }
}