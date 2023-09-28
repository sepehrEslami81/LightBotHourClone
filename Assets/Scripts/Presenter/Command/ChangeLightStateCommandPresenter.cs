using System.Collections;
using Model.Robot;
using Presenter.Cube;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Command
{
    public class ChangeLightStateCommandPresenter : MonoBehaviour, ICommand
    {
        [SerializeField] private RobotModel robotModel;
        [Range(0, 3f)] [SerializeField] private float delayTime = .2f;

        public IEnumerator Execute()
        {
            var robotTile = TileMapPresenter.GetTileByPosition(robotModel.Position);

            if (robotTile == null)
            {
                Debug.LogError("Tile not found");
                yield break; 
            }

            if (!robotTile.IsLightTile)
            {
                Debug.LogError("Tile is not a light tile");
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

            yield return new WaitForSeconds(delayTime);
        }
    }
}