using System.Collections;
using Model.Commands;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presenter.Command
{
    public abstract class Command : MonoBehaviour
    {
        [SerializeField] private CommandNames commandName;

        public CommandNames CommandName => commandName;
        
        public abstract IEnumerator Execute();
    }
}