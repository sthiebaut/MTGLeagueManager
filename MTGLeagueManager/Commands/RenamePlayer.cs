using System;

namespace MTGLeagueManager.Commands
{
    public class RenamePlayer : Command
    {
        public readonly Guid PlayerId;
        public readonly string NewName;

        public RenamePlayer(){}

        public RenamePlayer(Guid playerId, string newName)
        {
            PlayerId = playerId;
            NewName = newName;
        }
    }
}