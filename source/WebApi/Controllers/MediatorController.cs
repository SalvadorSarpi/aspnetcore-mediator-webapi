using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MediatorController : ControllerBase
	{

		IMediator mediator;

		public MediatorController(IMediator mediator)
		{
			this.mediator = mediator;
		}


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

	}
}
