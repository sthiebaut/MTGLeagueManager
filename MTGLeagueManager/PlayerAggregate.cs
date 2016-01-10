using System;
using System.Collections;
using MTGLeagueManager.Commands;
using MTGLeagueManager.Core;
using MTGLeagueManager.Events;
using MTGLeagueManager.ReadModel;
using MTGLeagueManager.Repository;

namespace MTGLeagueManager
{
    public class PlayerAggregate : Aggregate, 
        IHandleCommand<CreatePlayer>, IHandleCommand<RenamePlayer>, IHandleCommand<RemovePlayer>,
        IApplyEvent<PlayerCreated>, IApplyEvent<PlayerRemoved>, IApplyEvent<PlayerRenamed>
    {
        private readonly IPlayerRepository _repository;

        public PlayerAggregate()
        {
            _repository = new InMemoryPlayerRepository();
        }

        public PlayerAggregate(IPlayerRepository repository)
        {
            _repository = repository;
        }

        #region Command Handlers

        public IEnumerable Handle(CreatePlayer c)
        {
            if (_repository.GetByIdAsync(c.Id) != null)
                throw new PlayerAlreadyCreated();

            _repository.AddAsync(new Player { Id = c.Id, Name = c.Name });

            yield return new PlayerCreated(c.Id, c.Name);
        }

        public IEnumerable Handle(RenamePlayer c)
        {
            if (_repository.GetByIdAsync(c.Id) == null)
                throw new PlayerNotExist();

            _repository.RenameAsync(c.Id, c.NewName);

            yield return new PlayerRenamed(c.Id, c.NewName);
        }

        public IEnumerable Handle(RemovePlayer c)
        {
            if (_repository.GetByIdAsync(c.Id) == null)           
                throw new PlayerNotExist();

            _repository.RemoveAsync(c.Id);

            yield return new PlayerRemoved(c.Id);
        }
        
        #endregion

        public void Apply(PlayerCreated e) { }

        public void Apply(PlayerRemoved e) { }

        public void Apply(PlayerRenamed e) { }
    }
}
