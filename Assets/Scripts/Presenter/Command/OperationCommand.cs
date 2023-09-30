using System.Collections;
using Model.Commands;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    /// An abstract class for creating commands. You can design new commands by inheriting from this class
    /// </summary>
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