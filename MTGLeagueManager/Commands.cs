using System;

namespace MTGLeagueManager
{
    public class Command : Message
    {
    }

    public class CreatePlayer : Command
    {
        public readonly Guid Id;
        public readonly string Name;

        public CreatePlayer(){}

        public CreatePlayer(Guid playerId, string name)
        {
            Id = playerId;
            Name = name;
        }
    }

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

    public class RemovePlayer : Command
    {
        public Guid PlayerId;

        public RemovePlayer(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}
