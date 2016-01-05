using System;

namespace MTGLeagueManager.Events
{
    public class PlayerRenamed : Event
    {
        public readonly Guid Id;
        public readonly string NewName;

        public PlayerRenamed(){ }

        public PlayerRenamed(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}