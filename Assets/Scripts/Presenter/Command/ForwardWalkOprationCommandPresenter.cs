﻿using System.Collections;
using Model.Robot;
using Presenter.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class ForwardWalkOprationCommandPresenter : OprationCommand
    {
        [SerializeField] private RobotModel robotModel;
        [SerializeField] private RobotPresenter robotPresenter;

        public override IEnumerator Execute()
        {
            var nextPosition = robotPresenter.GetNextTilePosByDirection();
            var tile = TileMapPresenter.GetTileByPosition(robotModel.Position + nextPosition);

            if (tile == null)
            {
                Debug.Log("end of path!");
                yield break;
            }

            var robotY = robotModel.CurrentRobotYAxis;
            if (robotY - tile.Height != 0)
            {
                Debug.Log("They are not at the same height");
                yield break;
            }

            yield return robotPresenter.Move(tile.Position, tile.WorldPosition, tile.Height);
        }
    }
}