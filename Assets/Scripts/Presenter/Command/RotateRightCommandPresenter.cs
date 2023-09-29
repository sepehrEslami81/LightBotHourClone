using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class RotateRightCommandPresenter : Command
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        public override IEnumerator Execute()
        {
            Debug.Log("Exec: Rotate right");
            yield return robotPresenter.Rotate(RobotDirection.Right);

        }
    }
}