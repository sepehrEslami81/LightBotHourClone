using System.Collections.Generic;
using Model.Level;
using Presenter.Command;

namespace Presenter.Procedure
{
    public class Procedure
    {
        private readonly List<Command.OprationCommand> _commands;
        private readonly ProcedureModel _model;

        public IEnumerable<Command.OprationCommand> Commands => _commands;
        
        public Procedure(ProcedureModel model)
        {
            _model = new ProcedureModel();
            _commands = new List<Command.OprationCommand>();
        }

        internal bool AddCommand(Command.OprationCommand oprationCommand)
        {
            if (_model.MaximumCommands > 0 && _commands.Count >= _model.MaximumCommands)
                return false;
            
            _commands.Add(oprationCommand);
            return true;
        }

        internal void RemoveCommand(Command.OprationCommand oprationCommand)
        {
            _commands.Remove(oprationCommand);
        }
        
    }
}