using System;
using Edument.CQRS;
using MTGLeagueManager;
using MTGLeagueManager.Commands;
using MTGLeagueManager.Events;
using NUnit.Framework;

namespace LeagueManager.Test
{
    public class PlayerTest : BDDTest<PlayerAggregate>
    {
        private Guid _testId;
        private string _name;

        [SetUp]
        public void Setup()
        {
            _testId = Guid.NewGuid();
            _name = "Derek";
        }

        [Test]
        public void CanCreateNewPlayer()
        {
            Test(
                Given(),
                When(new CreatePlayer(_testId, _name)),
                Then(new PlayerCreated(_testId, _name))
            );
        }


        [Test]
        public void CannotCreateSamePlayerTwice()
        {
            Test(
                Given(new PlayerCreated(_testId, _name)),
                When(new CreatePlayer(_testId, _name)),
                ThenFailWith<PlayerAlreadyCreated>()
            );
        }



        [Test]
        public void CannotRenameWithNotCreatedPlayer()
        {
            Test(
                Given(),
                When(new RenamePlayer(_testId, "newName")),
                ThenFailWith<PlayerNotExist>());
        }

        [Test]
        public void CanRenameAnExistingPlayer()
        {
            Test(
                Given(new PlayerCreated(_testId, _name)),
                When(new RenamePlayer(_testId, _name)),
                Then(new PlayerRenamed(_testId, _name))
            );
        }

        [Test]
        public void CannotRemoveANotCreatedPlayer()
        {
            Test(
                Given(),
                When(new RemovePlayer(_testId)),
                ThenFailWith<PlayerNotExist>()
            );
        }

      
        [Test]
        public void CanRemoveAnExistingPlayer()
        {
            Test(
                Given(new PlayerCreated(_testId, _name)),
                When(new RemovePlayer(_testId)),
                Then(new PlayerRemoved(_testId))
            );
        }

        [Test]
        public void CannotRemoveAPlayerTwice()
        {
            Test(
                 Given(new PlayerCreated(_testId, _name),
                       new PlayerRemoved(_testId)),
                When(new RemovePlayer(_testId)),
                ThenFailWith<PlayerNotExist>()
            );
        }

    }
}
