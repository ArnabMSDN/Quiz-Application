using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz_Application.Web.Models;
using Microsoft.Extensions.Logging;
using Quiz_Application.Services;
using Quiz_Application.Services.Repository;
using Quiz_Application.Web.Common;
using Quiz_Application.Web.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Quiz_Application.Web.Controllers
{
    
    public class AccountController : Controller
    {        
        private readonly ILogger<AccountController> _logger;
        private readonly ICandidate<Services.Entities.Candidate> _candidate;

        public AccountController(ILogger<AccountController> logger,ICandidate<Services.Entities.Candidate> Candidate)
        {
            _candidate = Candidate;
            _logger = logger;
        }

        // GET: AccountController
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return PartialView("_Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel objCollection)
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: AccountController
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            string _Action = string.Empty;
            string _Controller = string.Empty;
            string value = Convert.ToString(HttpContext.Session.GetString("AuthenticatedUser"));

            if (string.IsNullOrEmpty(value))            
                return PartialView("_Login");            
            else                          
                return RedirectToAction("Index", "Home");                      
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Login([FromForm]LoginViewModel objCollection)
        {
            string _Action = string.Empty;
            string _Controller = string.Empty;
            string value =Convert.ToString(HttpContext.Session.GetString("AuthenticatedUser"));

            if(string.IsNullOrEmpty(value))
            {
                IQueryable<Services.Entities.Candidate> candidate = await _candidate.IsValidCandidate(x => x.Email.Equals(objCollection.Email) && x.Password.Equals(objCollection.Password));
                if (candidate.Any())
                {
                    HttpContext.Session.SetObjectAsJson("AuthenticatedUser", candidate.FirstOrDefault());
                    _Controller = "Home";
                    _Action = "Index";                   
                }
            }           
            else
            {
                _Controller = "Account";
                _Action = "Login";
            }
             return RedirectToAction(_Action, _Controller);
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            try
            {               
                foreach (var cookie in Request.Cookies.Keys) { Response.Cookies.Delete(cookie); }
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message, ex.InnerException);
            }
        }
    }
}
