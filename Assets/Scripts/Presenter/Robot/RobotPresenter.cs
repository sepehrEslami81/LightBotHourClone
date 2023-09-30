using System;
using System.Collections;
using System.Linq;
using Model;
using Model.Level;
using Model.Robot;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Robot
{
    public class RobotPresenter : MonoBehaviour
    {
        #region PRIVATE_FIELDS

        [Header("Lerp speed settings")] [Range(1, 100), SerializeField]
        private float lerpPosSpeed = 60;

        [Range(1, 360), SerializeField] private float lerpRotSpeed = 60;

        private RobotModel _robotModel;

        #endregion

        #region UNITY_METHODS

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

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Based on the current position of the robot, it measures the next position of the robot
        /// </summary>
        /// <returns>next position to move</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Position GetNextTilePosByDirection()
        {
            return _robotModel.Direction switch
            {
                RobotDirection.Forward => new Position(0, -1),
                RobotDirection.Left => new Position(1, 0),
                RobotDirection.Backward => new Position(0, 1),
                RobotDirection.Right => new Position(-1, 0),

                // just for fix rider warning
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// lerp robot to another tile position
        /// </summary>
        /// <param name="model">next tile model</param>
        /// <returns></returns>
        public IEnumerator Move(CubeTileModel model)
        {
            var targetWorldPos = CreatePosition(model.WorldPosition, model.Height, _robotModel.RobotHeight);

            float t = 0f;
            float moveDuration = Mathf.Abs(90f / lerpPosSpeed);

            while (t < moveDuration)
            {
                var a = _robotModel.CurrentWorldPosition;
                LerpPosition(a, targetWorldPos, t / moveDuration);

                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            FixPosition(targetWorldPos, model.Position);

            Vector3 CreatePosition(Vector3 pos, int tHeight, float rHeight) =>
                new Vector3(pos.x, tHeight + rHeight, pos.z);
        }

        /// <summary>
        /// rotate robot to target direction
        /// </summary>
        /// <param name="direction">target direction</param>
        /// <returns></returns>
        public IEnumerator Rotate(RobotDirection direction)
        {
            Vector3 startRotation = _robotModel.RobotGameObject.rotation.eulerAngles;
            float targetRotationY = startRotation.y + DirectionToAngel(direction).y;
            Vector3 targetRotation = new Vector3(0, targetRotationY, 0);

            float t = 0f;
            float rotationDuration = Mathf.Abs(90f / lerpRotSpeed);

            while (t < rotationDuration)
            {
                EulerRotationByLerp(startRotation, targetRotation, t / rotationDuration);

                t += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            FixRotation(targetRotation, direction);
        }

        /// <summary>
        /// reset robot at start position and direction
        /// </summary>
        public void ResetRobot()
        {
            ResetPosition();
            ResetRotation();
        }

        #endregion


        #region PRIVATE_METHODS

        /// <summary>
        /// reset robot position according by start direction
        /// </summary>
        private void ResetRotation()
        {
            var dir = LevelPresenter.CurrentLevel.StartRobotDirection;
            var angel = DirectionToAngel(dir);

            FixRotation(angel, dir);
            _robotModel.Direction = dir;
        }

        /// <summary>
        /// reset robot position according by start position
        /// </summary>
        private void ResetPosition()
        {
            var startPos = GetStartPosition();
            var tile = TileMapPresenter.GetTileByPosition(startPos);
            FixPosition(tile.WorldPosition, tile.Position);

            Position GetStartPosition() =>
                LevelPresenter.CurrentLevel.CubeTileModels.First(c => c.IsStartPoint).Position;
        }


        /// <summary>
        /// calculate to euler according by vector angel
        /// </summary>
        /// <param name="startRotation">from angel</param>
        /// <param name="targetRotation">target angel</param>
        /// <param name="rotationDuration">lerp time</param>
        private void EulerRotationByLerp(Vector3 startRotation, Vector3 targetRotation, float rotationDuration)
        {
            Vector3 currentRotation = Vector3.Lerp(startRotation, targetRotation, rotationDuration);
            _robotModel.RobotGameObject.rotation = Quaternion.Euler(currentRotation);
        }

        /// <summary>
        /// lerp position
        /// </summary>
        /// <param name="a">start position</param>
        /// <param name="b">target position</param>
        /// <param name="t">lerp time</param>
        private void LerpPosition(Vector3 a, Vector3 b, float t)
        {
            var lerp = Vector3.Lerp(a, b, t);
            _robotModel.RobotGameObject.transform.position = lerp;
        }

        /// <summary>
        /// Adjusts the position of the robot accurately
        /// </summary>
        /// <param name="targetWorldPos"></param>
        /// <param name="targetPos"></param>
        private void FixPosition(Vector3 targetWorldPos, Position targetPos)
        {
            _robotModel.Position = targetPos;
            var currentTile = TileMapPresenter.GetTileByPosition(targetPos);

            var y = CalculateYAxis(currentTile);
            var worldPos = new Vector3(targetWorldPos.x, y, targetWorldPos.z);
            _robotModel.RobotGameObject.transform.position = worldPos;

            float CalculateYAxis(CubeTileModel model) => _robotModel.RobotHeight + model.Height;
        }

        /// <summary>
        /// Adjusts the rotation of the robot accurately
        /// </summary>
        /// <param name="targetAngles"></param>
        /// <param name="dir"></param>
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

        /// <summary>
        /// Based on the direction, it measures the direction
        /// </summary>
        /// <param name="dir">target direction</param>
        /// <returns>vector angel</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        #endregion
    }
}