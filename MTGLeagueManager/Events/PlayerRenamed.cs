using System;

namespace MTGLeagueManager.Events
{
    public class PlayerRenamed : Event
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }

        public PlayerRenamed(){ }

        public PlayerRenamed(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}