using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz_Application.Web.Models;
using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Quiz_Application.Web.Extensions;
using Quiz_Application.Services.Entities;
using Quiz_Application.Web.Authentication;
using Quiz_Application.Services.Repository.Interfaces;
using Quiz_Application.Web.Enums;

namespace Quiz_Application.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICandidate<Services.Entities.Candidate> _candidate;

        public HomeController(ILogger<HomeController> logger, ICandidate<Services.Entities.Candidate> candidate)
        {
            _logger = logger;
            _candidate = candidate;
        }

        [BasicAuthentication]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            Candidate objCandidate = HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            return View(objCandidate);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            Candidate objCandidate = HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            ProfileViewModel objModel = new ProfileViewModel()
            {
                Sl_No = objCandidate.Sl_No,
                Name = objCandidate.Name,
                Candidate_ID = objCandidate.Candidate_ID,
                Email = objCandidate.Email,
                Phone = objCandidate.Phone,
                ImgFile = objCandidate.ImgFile
            };
            return View(objModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([FromForm] ProfileViewModel argObj)
        {
            int i = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles/Image");
                    string UniqueFileName = Guid.NewGuid().ToString() + "_" + argObj.file.FileName;
                    string UploadPath = Path.Combine(UploadFolder, UniqueFileName);

                    Candidate _objCandidate = await _candidate.GetCandidate(argObj.Sl_No);
                    _objCandidate.Name = argObj.Name;
                    _objCandidate.Candidate_ID = argObj.Candidate_ID;
                    _objCandidate.Phone = argObj.Phone;
                    _objCandidate.Email = argObj.Email;
                    _objCandidate.ImgFile = UniqueFileName;
                    _objCandidate.ModifiedBy = argObj.Name;
                    _objCandidate.ModifiedOn = DateTime.Now;
                    
                    i = await _candidate.UpdateCandidate(_objCandidate);
                    if (i > 0)
                    {
                        await argObj.file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
                        return RedirectToAction("Logout", "Account");
                    }
                    else
                    {
                        ViewBag.Alert = CommonService.ShowAlert(Alerts.Danger, "Unknown error");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            return View("Profile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public async Task<IActionResult> SaveImage()
        //{
        //    try
        //    {
        //        if (Request.Form.Files.Any())
        //        {
        //            string candidateID = Convert.ToString(Request.Form["Candidate-ID"]);
        //            var file = Request.Form.Files["Candidate-Img"];

        //            IQueryable<Services.Entities.Candidate> candidate = await _candidate.SearchCandidate(x => x.Candidate_ID == candidateID.DecodeBase64());
        //            if (candidate.Any())
        //            {
        //                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles/Image");
        //                string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
        //                var item = candidate.FirstOrDefault();
        //                item.ImgFile = UniqueFileName;
        //                await _candidate.UpdateCandidate(item);
        //                await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
        //            }
        //        }
        //        return Json(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex.InnerException);
        //    }
        //    finally
        //    { }
        //}

    }
}
