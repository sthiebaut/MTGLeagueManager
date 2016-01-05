using System;

namespace MTGLeagueManager.Events
{
    public class PlayerCreated : Event
    {
        public Guid Id;
        public string Name;

        public PlayerCreated(){}

        public PlayerCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}