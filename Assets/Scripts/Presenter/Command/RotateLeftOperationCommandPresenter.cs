using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    /// Used to turn left
    /// </summary>
    public class RotateLeftOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        /// <summary>
        /// Used to turn left
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Execute()
        {
            yield return robotPresenter.Rotate(RobotDirection.Left);
        }
    }
}