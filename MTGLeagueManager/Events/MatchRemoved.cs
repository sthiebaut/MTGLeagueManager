using System;

namespace MTGLeagueManager.Events
{
    public class MatchRemoved : Event
    {
        public readonly Guid Id;

        public MatchRemoved() { }

        public MatchRemoved(Guid id)
        {
            Id = id;
        }
    }

    public class MatchPlayed : Event
    {
        public readonly int Score1;
        public readonly int Score2;
        public readonly Guid Id;

        public MatchPlayed()
        {
        }

        public MatchPlayed(Guid id, int score1, int score2)
        {
            this.Id = id;
            this.Score1 = score1;
            this.Score2 = score2;
        }
    }
}