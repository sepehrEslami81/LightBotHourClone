using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Level;
using Presenter.Command;

namespace Presenter.Procedure
{
    /// <summary>
    /// This class is responsible for managing and maintaining procedure commands.
    /// </summary>
    public class Procedure
    {
        private readonly ProcedureModel _model;
        private readonly Dictionary<int, OperationCommand> _commands;

        private int _lastGeneratedId;

        private OperationCommand[] Commands => _commands
            .OrderBy(c=>c.Key)
            .Select(c => c.Value).ToArray();

        public Procedure(ProcedureModel model)
        {
            _model = model;
            _commands = new Dictionary<int, OperationCommand>();
        }
        
        /// <summary>
        /// It executes all the commands in its memory in order
        /// </summary>
        /// <returns></returns>
        public IEnumerator RunProcedure()
        {
            foreach (var command in Commands)
            {
                yield return command.Execute();
            }
        }

        /// <summary>
        /// Adds a new command. Of course, if that command can be added. (may have a limit on the number of commands)
        /// </summary>
        /// <param name="operationCommand">selected command nane</param>
        /// <returns></returns>
        internal bool AddCommand(OperationCommand operationCommand)
        {
            // 0 means we can add infinite commands
            if (_model.MaximumCommands > 0 && _commands.Count >= _model.MaximumCommands)
                return false;

            // create id for command
            int index = _lastGeneratedId++;
            
            _commands.Add(index, operationCommand);
            return true;
        }

        
        /// <summary>
        /// remove command from this procedure
        /// </summary>
        /// <param name="index">command id (index)</param>
        internal void RemoveCommand(int index)
        {
            _commands.Remove(index);
        }
    }
}