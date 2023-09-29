﻿using System.Collections;
using Model.Commands;
using UnityEngine;

namespace Presenter.Command
{
    public abstract class OperationCommand : MonoBehaviour
    {
        [SerializeField] private CommandNames commandName;

        public CommandNames CommandName => commandName;
        
        public abstract IEnumerator Execute();
    }
}