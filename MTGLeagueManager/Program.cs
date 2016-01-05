using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace MTGLeagueManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            var connection =
                EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            connection.ConnectAsync().Wait();

            var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
                                        Encoding.UTF8.GetBytes("some data"),
                                        Encoding.UTF8.GetBytes("some metadata"));

            var myEvent2 = new EventData(Guid.NewGuid(), "otherTestEvent", false,
                                        Encoding.UTF8.GetBytes("some other data"),
                                        Encoding.UTF8.GetBytes("some other metadata"));

            connection.AppendToStreamAsync("test-stream", ExpectedVersion.Any, myEvent).Wait();
            connection.AppendToStreamAsync("test-stream", ExpectedVersion.Any, myEvent2).Wait();

            var streamEvents = connection.ReadStreamEventsForwardAsync("test-stream", 0, 20, false).Result;

            foreach (var resolvedEvent in streamEvents.Events)
            {
                var returnedEvent = resolvedEvent.Event;

                Console.WriteLine("Read event with data: {0}, metadata: {1}",
                    Encoding.UTF8.GetString(returnedEvent.Data),
                    Encoding.UTF8.GetString(returnedEvent.Metadata));
            }
           

            Console.ReadLine();
        }
    }
}
