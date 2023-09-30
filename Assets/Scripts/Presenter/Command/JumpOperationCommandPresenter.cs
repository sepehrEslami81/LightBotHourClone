using System.Collections;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    /// it used to jump
    /// </summary>
    public class JumpOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private RobotPresenter robotPresenter;

        
        /// <summary>
        /// This command is used to jump to a higher height than yourself
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Execute()
        {
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
                    yield return robotPresenter.Move(tile);
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