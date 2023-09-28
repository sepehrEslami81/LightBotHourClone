using System.Collections;
using Model;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class ForwardWalkCommandPresenter : MonoBehaviour, ICommand
    {
        private RobotModel _robotModel;
        private RobotPresenter _robotPresenter;

        private void Awake()
        {
            LoadRobotModel();
            LoadRobotPresenter();
        }


        public IEnumerator Execute()
        {
            var tile = TileMapPresenter.GetTileByPosition(_robotModel.Position + new Position(0, 1));
            if (tile != null)
            {
                yield return StartCoroutine(_robotPresenter.Walk(tile.Position, tile.WorldPosition, tile.Height));
            }
            else
            {
                Debug.Log("end of path!");
                yield return null;
            }
            
        }


        private void LoadRobotModel()
        {
            if (TryGetComponent(out RobotModel model))
            {
                _robotModel = model;
            }
            else
            {
                Debug.LogError("failed to load robot model.");
            }
        }

        private void LoadRobotPresenter()
        {
            if (TryGetComponent(out RobotPresenter presenter))
            {
                _robotPresenter = presenter;
            }
            else
            {
                Debug.LogError("failed to load robot model.");
            }
        }
    }
}