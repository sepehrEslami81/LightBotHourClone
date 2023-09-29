﻿using System.Collections;
using Model.Robot;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Command
{
    public class RotateLeftCommandPresenter : Command
    {
        [SerializeField] private RobotPresenter robotPresenter;
        
        public override IEnumerator Execute()
        {
            Debug.Log("Exec: Rotate left");
            yield return robotPresenter.Rotate(RobotDirection.Left);
        }
    }
}