using System.Collections;
using Model;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presenter.Command
{
    public class ForwardWalkCommandPresenter : MonoBehaviour, ICommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private RobotPresenter robotPresenter;

        public IEnumerator Execute()
        {
            var nextPosition = robotPresenter.GetNextTilePosByDirection();
            var tile = TileMapPresenter.GetTileByPosition(robotModel.Position + nextPosition);
            if (tile != null)
            {
                yield return StartCoroutine(robotPresenter.Walk(tile.Position, tile.WorldPosition, tile.Height));
            }
            else
            {
                Debug.Log("end of path!");
            }
            
        }

    }
}