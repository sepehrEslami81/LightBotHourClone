using System.Collections.Generic;
using Presenter.Command;

namespace Presenter.Procedure
{
    public class Procedure
    {
        private readonly List<ICommand> _commands;

        public IEnumerable<ICommand> Commands => _commands;
        
        public Procedure()
        {
            _commands = new List<ICommand>();
        }

        internal void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        internal void RemoveCommand(ICommand command)
        {
            _commands.Remove(command);
        }
        
    }
}