using System;
using Edument.CQRS;
using MTGLeagueManager;
using NUnit.Framework;

namespace LeagueManager.Test
{

    public class PlayerTest : BDDTest<PlayerAggregate>
    {
        private Guid testId;
        private string name;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            name = "Derek";
        }

        [Test]
        public void CanCreateNewPlayer()
        {
            Test(
                Given(),
                When(new CreatePlayer(testId, name)),
                Then(new PlayerCreated(testId, name))
            );
        }

        [Test]
        public void CannotRenameWithNotCreatedPlayer()
        {
            Test(
                Given(),
                When(new RenamePlayer(testId, "newName")),
                ThenFailWith<PlayerNotCreated>());
        }

        [Test]
        public void CanRenameAnExistingPlayer()
        {
            Test(
                Given(new PlayerCreated(testId, name)),
                When(new RenamePlayer(testId, name)),
                Then(new PlayerRenamed(testId, name))
            );
        }

        [Test]
        public void CannotRemoveANotCreatedPlayer()
        {
            Test(
                Given(),
                When(new RemovePlayer(testId)),
                ThenFailWith<PlayerNotCreated>()
            );
        }

      
        [Test]
        public void CanRemoveAnExistingPlayer()
        {
            Test(
                Given(new PlayerCreated(testId, name)),
                When(new RemovePlayer(testId)),
                Then(new PlayerRemoved(testId))
            );
        }

        [Test]
        public void CannotRemoveAPlayerTwice()
        {
            Test(
                 Given(new PlayerCreated(testId, name),
                       new PlayerRemoved(testId)),
                When(new RemovePlayer(testId)),
                ThenFailWith<PlayerAlreadyRemoved>()
            );
        }

    }
}
