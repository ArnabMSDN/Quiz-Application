using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz_Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Quiz_Application.Web.Authentication;
using Microsoft.AspNetCore.Http;
using Quiz_Application.Web.Common;
using Quiz_Application.Services.Entities;

namespace Quiz_Application.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [BasicAuthentication]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            Candidate objCandidate= HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            return View(objCandidate);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
