using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class RotateRightCommandPresenter : MonoBehaviour, ICommand
    {
        [SerializeField] private RobotPresenter robotPresenter;
        [Range(1, 360)] [SerializeField] private float rotationThreshold = 91f; // based on the tests
        
        public IEnumerator Execute()
        {
            Debug.Log("Exec: Rotate right");
            yield return robotPresenter.Rotate(RobotDirection.Right);

        }
    }
}