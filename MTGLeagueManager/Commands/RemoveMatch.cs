using System;

namespace MTGLeagueManager.Commands
{
    public class RemoveMatch : Command
    {
        public Guid Id;

        public RemoveMatch(Guid id)
        {
            Id = id;
        }
    }

    public class PlayMatch : Command
    {
        public Guid Id;
        public int Score1;
        public int Score2;

        public PlayMatch(Guid id, int score1, int score2)
        {
            Score1 = score1;
            Score2 = score2;
            Id = id;
        }
    }
}