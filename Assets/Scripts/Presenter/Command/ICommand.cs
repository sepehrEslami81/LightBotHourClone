using System.Collections;

namespace Presenter.Command
{
    public interface ICommand
    {
        IEnumerator Execute();
    }
}