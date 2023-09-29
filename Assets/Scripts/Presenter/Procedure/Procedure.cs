using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Level;
using Presenter.Command;

namespace Presenter.Procedure
{
    public class Procedure
    {
        private readonly ProcedureModel _model;
        private readonly Dictionary<int, OperationCommand> _commands;

        private int _lastGeneratedId;

        private IEnumerable<OperationCommand> Commands => _commands.Select(c => c.Value);

        public Procedure(ProcedureModel model)
        {
            _model = model;
            _commands = new Dictionary<int, OperationCommand>();
        }
        
        public IEnumerator RunProcedure()
        {
            foreach (var command in Commands)
            {
                yield return command.Execute();
            }
        }

        internal bool AddCommand(OperationCommand operationCommand)
        {
            if (_model.MaximumCommands > 0 && _commands.Count >= _model.MaximumCommands)
                return false;

            int index = _lastGeneratedId++;
            _commands.Add(index, operationCommand);
            return true;
        }

        internal void RemoveCommand(int index)
        {
            _commands.Remove(index);
        }
    }
}