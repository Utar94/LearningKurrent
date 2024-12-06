using EventStore.Client;
using System.Text;

namespace LearningKurrent.Infrastructure;

internal class Repository
{
  private const string connectionString = "esdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false";

  public async Task Test(CancellationToken cancellationToken)
  {
    EventStoreClientSettings settings = EventStoreClientSettings.Create(connectionString);
    EventStoreClient client = new(settings);

    EventData data = new(
      eventId: Uuid.NewUuid(),
      type: "EventType",
      data: Array.Empty<byte>(),
      metadata: Array.Empty<byte>(),
      contentType: "application/json");

    IWriteResult result = await client.AppendToStreamAsync(
      streamName: "some-stream",
      expectedState: StreamState.Any,
      eventData: [data],
      configureOperationOptions: null,
      deadline: null,
      userCredentials: null,
      cancellationToken: cancellationToken);

    result = await client.AppendToStreamAsync(
      streamName: "some-stream",
      expectedRevision: StreamRevision.FromInt64(7),
      eventData: [data],
      configureOperationOptions: null,
      deadline: null,
      userCredentials: null,
      cancellationToken: cancellationToken);

    EventStoreClient.ReadStreamResult events = client.ReadStreamAsync(
      direction: Direction.Forwards,
      streamName: "some-stream",
      revision: 10,
      maxCount: 20,
      resolveLinkTos: false,
      deadline: null,
      userCredentials: null,
      cancellationToken: cancellationToken);

    if (await events.ReadState == ReadState.StreamNotFound)
    {
      throw new InvalidOperationException("The stream 'some-stream' does not exist.");
    }
    else
    {
      await foreach (ResolvedEvent @event in events)
      {
        // NOTE(fpion): not necessary when not using ReadAllAsync
        //if (@event.Event.EventType.StartsWith('$'))
        //{
        //  continue;
        //}

        Console.WriteLine(Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
      }
    }
  }
}
