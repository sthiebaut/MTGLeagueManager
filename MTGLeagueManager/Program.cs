using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MTGLeagueManager.Commands;
using MTGLeagueManager.ReadModel;

namespace MTGLeagueManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** MTG League Manager *****");

            //var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            //  connection.ConnectAsync().Wait();
            //    connection.DeleteStreamAsync("MTGEventStore", 0);

            // use a connection string
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("mtgleaguemanager");
            var collection = database.GetCollection<Player>("players");

            collection.DeleteMany(new BsonDocument());

            var count = collection.CountAsync(new BsonDocument());
            Console.WriteLine("MongoDB : Players count : " + count.Result);

            Domain.Setup();

            var players = Domain.PlayerListQueries.GetPlayers();

            Console.WriteLine("Queries : Players count : {0}", players.Count);

            Console.ReadLine();

            Console.WriteLine("** Create 2 players **");

            Domain.Dispatcher.SendCommand(new CreatePlayer(Guid.NewGuid(), "Sylvain"));
            Domain.Dispatcher.SendCommand(new CreatePlayer(Guid.NewGuid(), "Arnault"));

            players = Domain.PlayerListQueries.GetPlayers();
            Console.WriteLine("Queries : Players count : {0}", players.Count);

            count = collection.CountAsync(new BsonDocument());
            Console.WriteLine("MongoDB : Players count : " + count.Result);

            Console.WriteLine("** Remove 1 player **");
            var p = players[0];
            Domain.Dispatcher.SendCommand(new RemovePlayer(p.Id));

            count = collection.CountAsync(new BsonDocument());
            Console.WriteLine("MongoDB : Players count : " + count.Result);

            //Console.WriteLine("** Try to remove same player twice **");
            //Domain.Dispatcher.SendCommand(new RemovePlayer(p.Id));

            count = collection.CountAsync(new BsonDocument());
            Console.WriteLine("MongoDB : Players count : " + count.Result);

            Console.WriteLine("** Rename player **");
            players = Domain.PlayerListQueries.GetPlayers();
            p = players[0];
            Domain.Dispatcher.SendCommand(new RenamePlayer(p.Id, "Mickael"));

            Console.WriteLine("Queries : Player renamed : {0}", p.Name);


            Console.WriteLine("***** END *****");
            Console.ReadLine();
        }
    }
}
