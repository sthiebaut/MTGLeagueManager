using MTGLeagueManager.Core;
using MTGLeagueManager.ReadModel;
using MTGLeagueManager.Repository;

namespace MTGLeagueManager
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IPlayerListQueries PlayerListQueries;
        
        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new Core.EventStore());

            var playerRepository = new PlayerRepository();
            Dispatcher.ScanInstance(new PlayerAggregate(playerRepository));

            PlayerListQueries = new PlayerList(playerRepository);
            Dispatcher.ScanInstance(PlayerListQueries);
        }
    }
}