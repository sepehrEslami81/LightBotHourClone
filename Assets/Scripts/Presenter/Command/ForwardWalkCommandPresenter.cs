using System.Collections;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class ForwardWalkCommandPresenter : MonoBehaviour, ICommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private RobotPresenter robotPresenter;
        [SerializeField] private float moveThreshold = 1.5f; // based on the test result

        public IEnumerator Execute()
        {
            Debug.Log("Exec: Execute forward");
            var nextPosition = robotPresenter.GetNextTilePosByDirection();
            var tile = TileMapPresenter.GetTileByPosition(robotModel.Position + nextPosition);
            if (tile != null)
            {
                var robotY = robotModel.CurrentRobotYAxis;
                if (robotY - tile.Height == 0)
                {
                    yield return robotPresenter.Move(tile.Position, tile.WorldPosition, tile.Height, moveThreshold);
                }
                else
                {
                    Debug.Log("They are not at the same height");
                    yield return null;
                }
            }
            else
            {
                Debug.Log("end of path!");
                yield return null;
            }
            
        }

    }
}