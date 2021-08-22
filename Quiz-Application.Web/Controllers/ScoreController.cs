using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_Application.Web.Controllers
{
    public class ScoreController : Controller
    {
        public IActionResult Result()
        {
            return View();
        }
    }
}
