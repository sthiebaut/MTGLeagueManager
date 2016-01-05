using System;

namespace MTGLeagueManager.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerAggregate GetById(Guid id)
        {
            return new PlayerAggregate();
        }

        public void Add(PlayerAggregate aggregateRoot)
        {
            
        }
    }
}
