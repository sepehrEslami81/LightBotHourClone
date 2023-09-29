using System.Collections.Generic;
using Model.Level;
using Presenter.Command;

namespace Presenter.Procedure
{
    public class Procedure
    {
        private readonly List<Command.Command> _commands;
        private readonly ProcedureModel _model;

        public IEnumerable<Command.Command> Commands => _commands;
        
        public Procedure(ProcedureModel model)
        {
            _model = new ProcedureModel();
            _commands = new List<Command.Command>();
        }

        internal bool AddCommand(Command.Command command)
        {
            if (_model.MaximumCommands > 0 && _commands.Count >= _model.MaximumCommands)
                return false;
            
            _commands.Add(command);
            return true;
        }

        internal void RemoveCommand(Command.Command command)
        {
            _commands.Remove(command);
        }
        
    }
}