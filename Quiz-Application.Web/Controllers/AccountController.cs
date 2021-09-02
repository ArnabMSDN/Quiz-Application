using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Quiz_Application.Web.Models;
using Quiz_Application.Web.Extensions;
using Quiz_Application.Web.Enums;
using Quiz_Application.Services.Entities;
using Quiz_Application.Services.Repository.Interfaces;


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
            if (TempData["Message"] != null)
            {
                TempData["Message"] = null;
            }
            return PartialView("_Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel objCollection)
        {
            int i = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    IQueryable<Candidate> _iqCandidate = await _candidate.SearchCandidate(e => e.Candidate_ID.Equals(objCollection.Candidate_ID));
                    if (!_iqCandidate.Any())
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

                        i = await _candidate.AddCandidate(_objcandidate);

                        if (i > 0)
                            return RedirectToAction("Login", "Account");
                        else
                            TempData["Message"] = "An error occurred.";
                    }
                    else
                        TempData["Message"] = "A user already exists with that Candidate ID.";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            return PartialView("_Register");
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
            {
            }
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

                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        IQueryable<Services.Entities.Candidate> candidate = await _candidate.SearchCandidate(x => x.Email.Equals(objCollection.Email) && x.Password.Equals(objCollection.Password.EncodeBase64()));
                        if (candidate.Any())
                        {
                            Services.Entities.Candidate _candidate = new Services.Entities.Candidate();
                            _candidate = candidate.FirstOrDefault();
                            _candidate.Password = _candidate.Password.EncodeBase64();
                            HttpContext.Session.SetObjectAsJson("AuthenticatedUser", _candidate);
                            _Controller = "Home";
                            _Action = "Index";
                        }
                        else
                        {
                            TempData["Message"] = "Invalid User.";
                            _Controller = "Account";
                            _Action = "Login";
                        }
                    }
                    else
                    {
                        _Controller = "Account";
                        _Action = "Login";
                    }
                }
                return RedirectToAction(_Action, _Controller, ViewBag.Alert);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
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
            {
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Reset()
        {
            if (TempData["Message"] != null)
            {
                TempData["Message"] = null;
            }
            return PartialView("_Reset");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset([FromForm] ResetViewModel objCollection)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                IQueryable<Candidate> _iqCandidate = await _candidate.SearchCandidate(e => e.Email.Equals(objCollection.Email));
                if (_iqCandidate.Any())
                {
                    Candidate objCandidate = _iqCandidate.FirstOrDefault();
                    objCandidate.Password = objCollection.Password.EncodeBase64();
                    objCandidate.ModifiedBy = objCollection.Email;
                    objCandidate.ModifiedOn = DateTime.Now;
                   
                    i = await _candidate.UpdateCandidate(objCandidate);

                    if (i > 0)
                        return RedirectToAction("Login", "Account");
                    else
                        TempData["Message"] = "An error occurred.";
                }
                else
                    TempData["Message"] = "Invalid Email.";
            }
            return PartialView("_Reset");
        }

    }
}
