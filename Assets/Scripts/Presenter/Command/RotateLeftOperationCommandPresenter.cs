using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class RotateLeftOperationCommandPresenter : OperationCommand
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        public override IEnumerator Execute()
        {
            yield return robotPresenter.Rotate(RobotDirection.Left);
        }
    }
}