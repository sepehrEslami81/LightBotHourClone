using System.Collections;
using Model.Robot;
using Presenter.Cube;
using Presenter.Level;
using Presenter.Ui;
using UnityEngine;

namespace Presenter.Command
{
    public class ChangeLightStateOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private CompleteLevelUiPresenter levelUiPresenter;

        [Header("settings")] [Range(0, 3f)] [SerializeField]
        private float delayTime = .2f;

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

            if (!robotTile.CubeTileGameObject.TryGetComponent(out CubeTilePresenter presenter))
            {
                Debug.LogError("Cube presenter not found");
                yield break;
            }

            var type = presenter.Type == CubeType.TurnedOffTile
                ? CubeType.TurnedOnTile
                : CubeType.TurnedOffTile;

            presenter.ChangeTileStatus(type);

            if (type == CubeType.TurnedOnTile)
                levelUiPresenter.CountOfTurnedOnLightCubes++;
            else
                levelUiPresenter.CountOfTurnedOnLightCubes--;
        }
    }
}