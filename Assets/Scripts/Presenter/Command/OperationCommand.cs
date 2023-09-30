using System.Collections;
using Model.Commands;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presenter.Command
{
    public abstract class OperationCommand : MonoBehaviour
    {
        [FormerlySerializedAs("commandName")] [SerializeField] private CommandName commandName;

        public CommandName CommandName => commandName;
        
        public abstract IEnumerator Execute();
    }
}