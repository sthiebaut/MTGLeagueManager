using System;
using System.Collections;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using ServiceStack.Text;

namespace MTGLeagueManager.Core
{
    public class EventStore : IEventStore
    {
        private static readonly string eventstore = "MTGEventStore2";

        public IEnumerable LoadEventsFor<TAggregate>(Guid id)
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            connection.ConnectAsync().Wait();
            
            var streamEvents = connection.ReadStreamEventsForwardAsync(eventstore, 0, 4096, false).Result;

            foreach (var resolvedEvent in streamEvents.Events)
            {
                var returnedEvent = resolvedEvent.Event;

                var body = Encoding.UTF8.GetString(returnedEvent.Data);
                /*Console.WriteLine("Read event with data: {0}, metadata: {1}, type:{2}",
                    body,
                    Encoding.UTF8.GetString(returnedEvent.Metadata),
                    returnedEvent.EventType);*/

                yield return DeserializeEvent(returnedEvent.EventType, body);
            }
        }


        private object DeserializeEvent(string typeName, string data)
        {
            var ser = Type.GetType(typeName);
            return JsonSerializer.DeserializeFromString(data, ser);
        }

        public void SaveEventsFor<TAggregate>(Guid id, int eventsLoaded, ArrayList newEvents)
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            connection.ConnectAsync().Wait();

            foreach (var newEvent in newEvents)
            {
                var myEvent = new EventData(Guid.NewGuid(), newEvent.GetType().AssemblyQualifiedName, true,
                    Encoding.UTF8.GetBytes(SerializeEvent(newEvent)),
                    Encoding.UTF8.GetBytes("Date : " + DateTime.UtcNow));
                

                connection.AppendToStreamAsync(eventstore, ExpectedVersion.Any, myEvent).Wait();
            }
        }

        private string SerializeEvent(object obj)
        {
            var serializeToString = JsonSerializer.SerializeToString(obj, obj.GetType());
            return serializeToString;
        }
    }
}