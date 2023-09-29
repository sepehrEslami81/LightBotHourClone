using System.Collections;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class JumpCommandPresenter : Command
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private RobotPresenter robotPresenter;

        
        public override IEnumerator Execute()
        {
            Debug.Log("Exec: Execute jump");

            var nextPos = robotPresenter.GetNextTilePosByDirection();
            var tile = TileMapPresenter.GetTileByPosition(robotModel.Position + nextPos);

            if (tile != null)
            {
                var currentRobotY = robotModel.CurrentRobotYAxis;
                if (
                    (tile.Height - currentRobotY == 1) || // check can jump from down to up
                    (currentRobotY - tile.Height > 0) // check can jump from up to down
                )
                {
                    yield return robotPresenter.Move(tile.Position, tile.WorldPosition, tile.Height);
                }
                else
                {
                    Debug.Log("cant jump");
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