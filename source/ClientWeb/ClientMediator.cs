using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ClientWeb
{

	public class ClientMediator : IMediator
	{

		public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
		{
			throw new NotImplementedException();
		}

		public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
		{
			var json = JsonConvert.SerializeObject(request);

			WebClient client = new WebClient();
			var r = await client.UploadStringTaskAsync("http://localhost:62173/api/Mediator", json);

			return (TResponse) Convert.ChangeType(r, typeof(TResponse));
		}
	}

}
