# aspnetcore-mediator-webapi

This is a simple example of a client and a web api that shares command definitions.
MediatR library is being used on both client and api to handle commands being sent over http transport.

The idea of this code is to be able to send commands from a client to a webapi that handles them using a single api controller method. Something like mediator pattern with an http transport in the middle.

## Try it out

Open the solution using Visual Studio 2017, set multiple startup projects: ClientWeb and WebApi.


# Some code

This is the shared command definition:

```csharp
[DataContract]
public class MessageCommand : IRequest<string>
{
	[DataMember]
	public string Message { get; private set; }

	public MessageCommand(string message)
	{
		this.Message = message;
	}
}
```

The command handler (sitting on the web api side) looks like this:
```csharp
public class MessageCommandHandler : IRequestHandler<MessageCommand, string>
{
	public Task<string> Handle(MessageCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult($"Handled: {request.Message}");
	}
}
```

The api controller method deserializes and calls de mediator to handle the command definition. This is just a proof of concept, no work on generics for command definitions has been done.
```csharp
[HttpPost]
public async Task<string> Post()
{
	using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
	{
		string body = await reader.ReadToEndAsync();
		var command = JsonConvert.DeserializeObject<MessageCommand>(body);
		return await mediator.Send(command);
	}
}
```

On the client side, there's also a mediator receiving commands to be dispatched to the api (this implements IMediator from MediatR library):
```csharp
public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
{
	var json = JsonConvert.SerializeObject(request);
	WebClient client = new WebClient();
	var r = await client.UploadStringTaskAsync("http://localhost:62173/api/Mediator", json);
	return (TResponse) Convert.ChangeType(r, typeof(TResponse));
}
```

Finally, the client code to call for a command to be executed:
```csharp
var result = await mediator.Send(new MessageCommand("hello from client"));
```

## Results

The command definition is composed on the client, then the client's mediator sends it to the api endpoint, the command is deserialized on the api and sent to the api's mediator to find and execute the right handler. The response is then returned to the client's mediator, then sent back to the caller.
