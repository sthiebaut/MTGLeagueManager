using System;
using Edument.CQRS;
using MTGLeagueManager;
using MTGLeagueManager.Commands;
using MTGLeagueManager.Events;
using NUnit.Framework;

namespace LeagueManager.Test
{
    public class MatchTest : BDDTest<MatchAggregate>
    {
        private Guid _playerId1;
        private Guid _playerId2;
        private Guid _matchId;

        [SetUp]
        public void Setup()
        {
            _playerId1 = Guid.NewGuid();
            _playerId2 = Guid.NewGuid();
            _matchId = Guid.NewGuid();
        }

        [Test]
        public void CanCreateNewMatch()
        {
            Test(
                Given(),
                When(new CreateMatch(_matchId, _playerId1, _playerId2, DateTime.Today)),
                Then(new MatchCreated(_matchId, _playerId1, _playerId2, DateTime.Today))
            );
        }

        [Test]
        public void CannotRemoveNonExistingMatch()
        {
            Test(
                Given(),
                When(new RemoveMatch(_matchId)),
                ThenFailWith<MatchNotCreated>()
            );
        }

        [Test]
        public void CanRemoveMatch()
        {
            Test(
                Given(new MatchCreated(_matchId, _playerId1, _playerId2, DateTime.Today)),
                When(new RemoveMatch(_matchId)),
                Then(new MatchRemoved(_matchId))
            );
        }

        [Test]
        public void CannotRemoveMatchTwice()
        {
            Test(
                Given(new MatchCreated(_matchId, _playerId1, _playerId2, DateTime.Today),
                      new MatchRemoved(_matchId)),
                When(new RemoveMatch(_matchId)),
                ThenFailWith<MatchAlreadyRemoved>()
            );
        }

        [Test]
        public void CanPlayExitingMatch()
        {
            Test(
                Given(new MatchCreated(_matchId, _playerId1, _playerId2, DateTime.Today)),
                When(new PlayMatch(_matchId, 2, 0)),
                 Then(new MatchPlayed(_matchId, 2, 0))
            );
        }

        [Test]
        public void CannotPlayMatchTwice()
        {
            Test(
                Given(new MatchCreated(_matchId, _playerId1, _playerId2, DateTime.Today),
                      new MatchPlayed(_matchId, 2, 0)),
                When(new PlayMatch(_matchId, 2, 0)),
                 ThenFailWith<MatchAlreadyPlayed>()
            );
        }

    }
}
