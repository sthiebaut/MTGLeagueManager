using System;

namespace MTGLeagueManager.Events
{
    public class PlayerCreated : Event
    {
        public Guid Id { get; set; }
    
        public string Name { get; set; }

        public PlayerCreated(){}

        public PlayerCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}