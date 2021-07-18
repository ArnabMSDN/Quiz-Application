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
using Quiz_Application.Services.Repository.Candidate;
using System.Security.Claims;


namespace Quiz_Application.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ICandidate<Services.Entities.Candidate> _candidate;

        public AccountController(ILogger<AccountController> logger, ICandidate<Services.Entities.Candidate> Candidate)
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
            Services.Entities.Candidate _objcandidate = new Services.Entities.Candidate()
            {
                Name = objCollection.Name,
                Email = objCollection.Email,
                Phone = objCollection.Phone,
                Candidate_ID = objCollection.Candidate_ID,
                Roles = "User",
                Password = objCollection.Password.EncodeBase64(),
                CreatedBy = "SYSTEM",
                CreatedOn = DateTime.Now
            };
            


            int i= await _candidate.InsertCandidate(_objcandidate);
            return RedirectToAction("Login", "Account");
        }

        // GET: AccountController
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            try
            {
                string _Action = string.Empty;
                string _Controller = string.Empty;
                string value = Convert.ToString(HttpContext.Session.GetString("AuthenticatedUser"));

                if (string.IsNullOrEmpty(value))
                    return PartialView("_Login");
                else
                    return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            { }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel objCollection)
        {
            try
            {
                string _Action = string.Empty;
                string _Controller = string.Empty;
                string value = Convert.ToString(HttpContext.Session.GetString("AuthenticatedUser"));

                if (string.IsNullOrEmpty(value))
                {
                    IQueryable<Services.Entities.Candidate> candidate = await _candidate.SearchCandidate(x => x.Email.Equals(objCollection.Email) && x.Password.Equals(objCollection.Password));
                    if (candidate.Any())
                    {
                        Services.Entities.Candidate _candidate = new Services.Entities.Candidate();
                        _candidate = candidate.FirstOrDefault();
                        _candidate.Password = _candidate.Password.EncodeBase64();
                        HttpContext.Session.SetObjectAsJson("AuthenticatedUser", _candidate);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            { }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }               
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            { }
        }
    }
}
