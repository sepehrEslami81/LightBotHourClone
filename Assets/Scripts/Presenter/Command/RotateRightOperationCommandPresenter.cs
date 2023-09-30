using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    /// Used to turn right
    /// </summary>
    public class RotateRightOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        /// <summary>
        /// Used to turn right
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Execute()
        {
            yield return robotPresenter.Rotate(RobotDirection.Right);

        }
    }
}