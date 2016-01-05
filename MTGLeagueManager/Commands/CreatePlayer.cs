using System;

namespace MTGLeagueManager.Commands
{
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
}