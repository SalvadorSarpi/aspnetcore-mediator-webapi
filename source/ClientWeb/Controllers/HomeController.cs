using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientWeb.Models;
using MediatR;
using Commands;

namespace ClientWeb.Controllers
{
	public class HomeController : Controller
	{

		IMediator mediator;

		public HomeController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		public async Task<IActionResult> Index()
		{
			var result = await mediator.Send(new MessageCommand("hello from client"));

			ViewBag.ApiMessage = result;

			return View();
		}

	}
}
