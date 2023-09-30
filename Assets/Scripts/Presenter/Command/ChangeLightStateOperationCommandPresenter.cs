using System.Collections;
using Model.Robot;
using Presenter.Cube;
using Presenter.Level;
using Presenter.Ui;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    ///  Used to light tiles that can be lit
    /// </summary>
    public class ChangeLightStateOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private CompleteLevelUiPresenter levelUiPresenter;

        [Header("settings")] [Range(0, 3f)] [SerializeField]
        private float delayTime = .2f;

        /// <summary>
        /// Used to light tiles that can be lit. If the robot is placed on this tile, it can turn on that tile
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(delayTime);

            var robotTile = TileMapPresenter.GetTileByPosition(robotModel.Position);

            if (robotTile == null)
            {
                Debug.Log("Tile not found");
                yield break;
            }

            if (!robotTile.IsLightTile)
            {
                Debug.Log("Tile is not a light tile");
                yield break;
            }

            var type = robotTile.CubeTilePresenter.Type == CubeType.TurnedOffTile
                ? CubeType.TurnedOnTile
                : CubeType.TurnedOffTile;

            ChangeCountOfTurnedOnLights(type);
            robotTile.CubeTilePresenter.ChangeTileStatus(type);
        }

        /// <summary>
        /// Changing the number of lit tiles
        /// </summary>
        /// <param name="type">current cube light type</param>
        private void ChangeCountOfTurnedOnLights(CubeType type)
        {
            if (type == CubeType.TurnedOnTile)
                LevelPresenter.CountOfTurnedOnLightCubes++;
            else
                LevelPresenter.CountOfTurnedOnLightCubes--;
        }
    }
}