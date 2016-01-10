using System;

namespace MTGLeagueManager.Commands
{
    public class RemovePlayer : Command
    {
        public Guid Id;

        public RemovePlayer(Guid id)
        {
            Id = id;
        }
    }
}
