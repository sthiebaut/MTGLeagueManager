using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MTGLeagueManager.ReadModel;

namespace MTGLeagueManager.Repository
{
    public interface IPlayerRepository
    {
        Task<Player> GetByIdAsync(Guid id);
        Task AddAsync(Player player);
        Task RemoveAsync(Guid id);

        Task RenameAsync(Guid id, string newName);
    }

    public class InMemoryPlayerRepository : IPlayerRepository
    {
        public List<Player> Players = new List<Player>();
        public Task<Player>  GetByIdAsync(Guid id)
        {
            return Task.Run(() => Players.Find(p => p.Id == id));
        }

        public Task AddAsync(Player player)
        {
            return Task.Run(() => Players.Add(player));
        }

        public Task RemoveAsync(Guid id)
        {
            return Task.Run(() => Players.RemoveAll(p => p.Id == id));
        }

        public Task RenameAsync(Guid id, string newName)
        {
            var player = GetByIdAsync(id);
            return Task.Run(() => player.Result.Name = newName);
        }
    }
}