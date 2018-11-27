using MediatR;
using System;
using System.Runtime.Serialization;

namespace Commands
{

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

}
