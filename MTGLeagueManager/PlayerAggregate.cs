using System;
using System.Collections;
using MTGLeagueManager.Commands;
using MTGLeagueManager.Core;
using MTGLeagueManager.Events;
using MTGLeagueManager.Repository;

namespace MTGLeagueManager
{
    public class PlayerAggregate : Aggregate, 
        IHandleCommand<CreatePlayer>, IHandleCommand<RenamePlayer>, IHandleCommand<RemovePlayer>,
        IApplyEvent<PlayerCreated>, IApplyEvent<PlayerRemoved>
    {
        public string Name { get; private set; }
        public bool IsRemoved { get; private set; }

        public PlayerAggregate()
        {
        }

        #region Command Handlers

        public IEnumerable Handle(CreatePlayer c)
        {
            if (Id == c.Id)
                throw new PlayerAlreadyCreated();

            yield return new PlayerCreated(c.Id, c.Name);
        }

        public IEnumerable Handle(RenamePlayer c)
        {
            if (Id == Guid.Empty)
                throw new PlayerNotCreated();

            yield return new PlayerRenamed(c.PlayerId, c.NewName);
        }

        public IEnumerable Handle(RemovePlayer c)
        {
            if (Id == Guid.Empty)
                throw new PlayerNotCreated();

            if (IsRemoved)
                throw new PlayerAlreadyRemoved();

            yield return new PlayerRemoved(c.PlayerId);
        }
        
        #endregion

        public void Apply(PlayerCreated e)
        {
            Id = e.Id;
            Name = e.Name;
        }

        public void Apply(PlayerRemoved e)
        {
            IsRemoved = true;
        }
    }
}
