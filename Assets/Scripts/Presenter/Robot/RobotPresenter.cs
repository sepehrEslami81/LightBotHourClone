using System;
using System.Collections;
using Model;
using Model.Robot;
using Presenter.Level;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

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
            float targetRotationY = startRotation.y + (direction == RobotDirection.Right ? 90f : -90f);
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
            _robotModel.RobotGameObject.transform.position = targetWorldPos;
        }

        private void FixRotation(Vector3 targetAngles, RobotDirection dir)
        {
            transform.eulerAngles = targetAngles;
            _robotModel.Direction = CalculateCurrentDirection(dir);


            RobotDirection CalculateCurrentDirection(RobotDirection rotedToDirection)
            {
                int directionNum = rotedToDirection == RobotDirection.Left ? 3 : 1;
                return (RobotDirection)((int)(_robotModel.Direction + directionNum) % 4);
            }
        }
    }
}