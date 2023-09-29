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
        private readonly Dictionary<int, OprationCommand> _commands;

        private int _lastGeneratedId;

        private IEnumerable<OprationCommand> Commands => _commands.Select(c => c.Value);

        public Procedure(ProcedureModel model)
        {
            _model = new ProcedureModel();
            _commands = new Dictionary<int, OprationCommand>();
        }
        
        public IEnumerator RunProcedure()
        {
            foreach (var command in Commands)
            {
                yield return command.Execute();
            }
        }

        internal bool AddCommand(OprationCommand oprationCommand)
        {
            if (_model.MaximumCommands > 0 && _commands.Count >= _model.MaximumCommands)
                return false;

            int index = _lastGeneratedId++;
            _commands.Add(index, oprationCommand);
            return true;
        }

        internal void RemoveCommand(int index)
        {
            _commands.Remove(index);
        }
    }
}