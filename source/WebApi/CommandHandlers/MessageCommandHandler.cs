using Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.CommandHandlers
{

	public class MessageCommandHandler : IRequestHandler<MessageCommand, string>
	{

		public Task<string> Handle(MessageCommand request, CancellationToken cancellationToken)
		{
			return Task.FromResult($"Handled: {request.Message}");
		}

	}

}
