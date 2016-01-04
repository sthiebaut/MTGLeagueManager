using System;

namespace MTGLeagueManager
{
    public class Event : Message
    {
        public int Version;
    }

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

    public class PlayerRemoved : Event
    {
        public readonly Guid Id;

        public PlayerRemoved() { }

        public PlayerRemoved(Guid id)
        {
            Id = id;
        }
    }

}
