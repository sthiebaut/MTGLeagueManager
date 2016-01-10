using System.Collections.Generic;
using MTGLeagueManager.Core;
using MTGLeagueManager.Events;
using MTGLeagueManager.Repository;

namespace MTGLeagueManager.ReadModel
{
    public class PlayerList : ISubscribeTo<PlayerCreated>, ISubscribeTo<PlayerRemoved>, ISubscribeTo<PlayerRenamed>, IPlayerListQueries
    {
        private readonly IPlayerRepository _repository;
        public List<Player> Players = new List<Player>();

        public PlayerList(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public List<Player> GetPlayers()
        {
            lock (Players)
            {
                return Players;
            }
        }

        public void Handle(PlayerCreated e)
        {
            lock (Players)
            {
                Players.Add(new Player() { Id = e.Id, Name = e.Name });
            }
        }

        public void Handle(PlayerRemoved e)
        {
            lock (Players)
            {
                Players.RemoveAll(p => p.Id == e.Id);
            }
        }

        public void Handle(PlayerRenamed e)
        {
            lock (Players)
            {
                var pl = Players.Find(p => p.Id == e.Id);
                pl.Name = e.NewName;
            }
        }
    }
}
