using System.Collections;
using Model.Commands;
using UnityEngine;

namespace Presenter.Command
{
    public abstract class OperationCommand : MonoBehaviour
    {
        [SerializeField] private CommandName commandName;

        public CommandName CommandName => commandName;
        
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator Execute();
    }
}