using System.Collections.Generic;

namespace MTGLeagueManager.ReadModel
{
    public interface IPlayerListQueries
    {
        List<PlayerItem> GetPlayers();
    }
}