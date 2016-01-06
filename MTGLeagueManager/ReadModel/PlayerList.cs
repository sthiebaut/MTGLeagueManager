using System;
using System.Collections.Generic;
using MTGLeagueManager.Core;
using MTGLeagueManager.Events;

namespace MTGLeagueManager.ReadModel
{
    public class PlayerList : ISubscribeTo<PlayerCreated>, ISubscribeTo<PlayerRemoved>, IPlayerListQueries
    {
        public List<PlayerItem> Players = new List<PlayerItem>();

        public List<PlayerItem> GetPlayers()
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
                Players.Add(new PlayerItem() { Id = e.Id, Name = e.Name });
            }
        }

        public void Handle(PlayerRemoved e)
        {
            lock (Players)
            {
                Players.RemoveAll(p => p.Id == e.Id);
            }
        }
    }

    public class PlayerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
