using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class RotateRightCommandPresenter : MonoBehaviour, ICommand
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        public IEnumerator Execute()
        {
            Debug.Log("Exec: Rotate right");
            yield return robotPresenter.Rotate(RobotDirection.Right);

        }
    }
}