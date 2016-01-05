using System;

namespace MTGLeagueManager.Commands
{
    public class RemovePlayer : Command
    {
        public Guid PlayerId;

        public RemovePlayer(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}
