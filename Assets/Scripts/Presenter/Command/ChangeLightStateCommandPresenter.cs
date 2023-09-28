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
            if (robotTile is not null)
            {
                if (robotTile.IsLightTile)
                {
                    if (robotTile.CubeTileGameObject.TryGetComponent(out CubeTilePresenter presenter))
                    {
                        CubeType type = presenter.Type == CubeType.TurnedOffTile
                            ? CubeType.TurnedOnTile
                            : CubeType.TurnedOffTile;

                        presenter.ChangeTileStatus(type);

                        yield return new WaitForSeconds(delayTime);
                    }
                    else
                    {
                        Debug.LogError("cube presenter not found");
                    }
                }
                else
                {
                    Debug.LogError("tile is not light tile");
                    yield return null;
                }
            }
            else
            {
                Debug.LogError("tile not found");
                yield return null;
            }
        }
    }
}