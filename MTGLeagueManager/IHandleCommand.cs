using System.Collections;

namespace MTGLeagueManager
{
    public interface IHandleCommand<in TCommand>
    {
        IEnumerable Handle(TCommand c);
    }
}