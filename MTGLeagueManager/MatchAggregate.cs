using System;
using System.Collections;
using MTGLeagueManager.Commands;
using MTGLeagueManager.Core;
using MTGLeagueManager.Events;

namespace MTGLeagueManager
{
    public class MatchAggregate : Aggregate,
        IHandleCommand<CreateMatch>, IHandleCommand<RemoveMatch>, IHandleCommand<PlayMatch>,
        IApplyEvent<MatchCreated>, IApplyEvent<MatchRemoved>, IApplyEvent<MatchPlayed>
    {
        public Guid Id;
        public DateTime Date;
        public Guid PlayerId1;
        public Guid PlayerId2;
        public bool Removed;
        public bool Played;


        public IEnumerable Handle(CreateMatch c)
        {
            yield return new MatchCreated(c.Id, c.PlayerId1, c.PlayerId2, c.Date);
        }

        public IEnumerable Handle(RemoveMatch c)
        {
            if (Id == Guid.Empty) throw new MatchNotCreated();

            if (Removed) throw new MatchAlreadyRemoved();

            yield return new MatchRemoved(c.Id);
        }

        public IEnumerable Handle(PlayMatch c)
        {
            if (Id == Guid.Empty) throw new MatchNotCreated();

            if (Removed) throw new MatchAlreadyRemoved();

            if (Played) throw new MatchAlreadyPlayed();

            yield return new MatchPlayed(c.Id, c.Score1, c.Score2);
        }

        public void Apply(MatchCreated e)
        {
            Id = e.Id;
            Date = e.Date;
            PlayerId1 = e.PlayerId1;
            PlayerId2 = e.PlayerId2;
        }

        public void Apply(MatchRemoved e)
        {
            Removed = true;
        }

        public void Apply(MatchPlayed e)
        {
            Played = true;


        }
    }
}