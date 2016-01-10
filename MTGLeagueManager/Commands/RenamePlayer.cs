using System;

namespace MTGLeagueManager.Commands
{
    public class RenamePlayer : Command
    {
        public readonly Guid Id;
        public readonly string NewName;

        public RenamePlayer(){}

        public RenamePlayer(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}