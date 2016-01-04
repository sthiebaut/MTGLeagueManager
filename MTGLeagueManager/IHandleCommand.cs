using System.Collections;

namespace MTGLeagueManager
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable Handle(TCommand c);
    }
}