using System;

namespace MTGLeagueManager.Repository
{
    public interface IPlayerRepository
    {
        PlayerAggregate GetById(Guid id);
        void Add(PlayerAggregate aggregateRoot);
        void Remove(Guid id);
    }
}