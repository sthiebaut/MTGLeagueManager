using System;

namespace MTGLeagueManager.Events
{
    public class MatchCreated : Event
    {
        public Guid Id;
        public DateTime Date;
        public Guid PlayerId1;
        public  Guid PlayerId2;

        public MatchCreated(){}

        public MatchCreated(Guid id, Guid playerId1, Guid playerId2, DateTime date)
        {
            Id = id;
            Date = date;
            PlayerId1 = playerId1;
            PlayerId2 = playerId2;
        }
    }
}