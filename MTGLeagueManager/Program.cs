using System;
using MTGLeagueManager.Commands;

namespace MTGLeagueManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            //var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
          //  connection.ConnectAsync().Wait();
        //    connection.DeleteStreamAsync("MTGEventStore", 0);

            Domain.Setup();

            var players = Domain.PlayerListQueries.GetPlayers();

            Console.WriteLine("Players : {0}", players.Count);

            Console.ReadLine();

            Domain.Dispatcher.SendCommand(new CreatePlayer(Guid.NewGuid(), "Sylvain"));

            players = Domain.PlayerListQueries.GetPlayers();
            Console.WriteLine("Players : {0}", players.Count);

            Console.ReadLine();
        }
    }
}
