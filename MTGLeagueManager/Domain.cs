using MTGLeagueManager.Core;
using MTGLeagueManager.ReadModel;

namespace MTGLeagueManager
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IPlayerListQueries PlayerListQueries;
        
        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new Core.EventStore());

            Dispatcher.ScanInstance(new PlayerAggregate());

            PlayerListQueries = new PlayerList();
            Dispatcher.ScanInstance(PlayerListQueries);

            
        }
    }
}