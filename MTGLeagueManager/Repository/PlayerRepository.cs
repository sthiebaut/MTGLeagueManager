using System;
using System.Collections.Generic;

namespace MTGLeagueManager.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        public List<PlayerAggregate> Players = new List<PlayerAggregate>();

        public PlayerAggregate GetById(Guid id)
        {
            return Players.Find(p => p.Id == id);
        }

        public void Add(PlayerAggregate aggregateRoot)
        {
            Players.Add(aggregateRoot);
        }

        public void Remove(Guid id)
        {
            Players.RemoveAll(p => p.Id == id);
        }
    }
}
