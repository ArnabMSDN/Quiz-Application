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
        public async Task<IActionResult> Index()
        {
            Candidate objHis = HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            IQueryable<Candidate> iqCandidate = await _candidate.SearchCandidate(e => e.Sl_No.Equals(objHis.Sl_No));
            Candidate objCandidate = iqCandidate.FirstOrDefault();
            return View(objCandidate);
        }       

        [BasicAuthentication]
        public async Task<IActionResult> Profile()
        {
            Candidate objHis= HttpContext.Session.GetObjectFromJson<Candidate>("AuthenticatedUser");
            IQueryable<Candidate> iqCandidate =await _candidate.SearchCandidate(e=>e.Sl_No.Equals(objHis.Sl_No));
            Candidate objCandidate = iqCandidate.FirstOrDefault();

            ProfileViewModel objModel = new ProfileViewModel()
            {
                Sl_No = objCandidate.Sl_No,
                Name = objCandidate.Name,
                Candidate_ID = objCandidate.Candidate_ID,
                Email = objCandidate.Email,
                Phone = objCandidate.Phone,
                ImgFile = objCandidate.ImgFile!=null ? objCandidate.ImgFile:null
            };                   
            return View(objModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([FromForm] ProfileViewModel argObj)
        {
            int i = 0;
            string UploadFolder = null;
            string UniqueFileName = null;
            string UploadPath = null;
            if (ModelState.IsValid)
            {                
                try
                {
                    if (argObj.file != null)
                    {
                        UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles/Image");
                        UniqueFileName = Guid.NewGuid().ToString() + "_" + argObj.file.FileName;
                        UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                    }
                    Candidate _objCandidate = await _candidate.GetCandidate(argObj.Sl_No);       
                    _objCandidate.Name = argObj.Name;
                    _objCandidate.Candidate_ID = argObj.Candidate_ID;
                    _objCandidate.Phone = argObj.Phone;
                    _objCandidate.Email = argObj.Email;
                    if (UniqueFileName != null)
                    { _objCandidate.ImgFile = UniqueFileName; }
                    else
                    { _objCandidate.ImgFile = _objCandidate.ImgFile; }
                    _objCandidate.ModifiedBy = argObj.Name;
                    _objCandidate.ModifiedOn = DateTime.Now;
                    argObj.ImgFile = _objCandidate.ImgFile;
                    i = await _candidate.UpdateCandidate(_objCandidate);
                    if (i > 0)
                    {
                        if (argObj.file != null)
                        {
                        await argObj.file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
                        }                        
                        ViewBag.Alert = AlertExtension.ShowAlert(Alerts.Success, "Profile updated successfully.");
                    }
                    else                    
                        ViewBag.Alert = AlertExtension.ShowAlert(Alerts.Danger, "An error occurred.");                    
                }
                catch (Exception ex)
                {
                    ViewBag.Alert = AlertExtension.ShowAlert(Alerts.Danger, ex.Message);
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
                ModelState.AddModelError("Error","Unknown  Error.");
            
            return View(argObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            if (Request.Form.Files.Any())
            {
                var file = Request.Form.Files["video-blob"];
                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles/Video");
                string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName + ".webm";
                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
            }
            return Json(HttpStatusCode.OK);
        }

    }
}
